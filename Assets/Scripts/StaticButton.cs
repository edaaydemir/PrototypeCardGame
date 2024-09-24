using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System.Threading;

[RequireComponent(typeof(RectTransform))]
public class StaticButton : Image, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Material buttonMaterial;
    [SerializeField] private Color buttonColor = Color.white;
    [SerializeField] private bool raycastTarget = true;
    [SerializeField] private Vector4 raycastPadding;
    public UnityEvent onClick;
    public UnityEvent onPress;
    public UnityEvent onReleased;

    private Vector3 originalScale;
    private float scaleDownFactor = 0.8f;
    private float animationDuration = 0.1f; // Duration for the scale animation

    // Cancellation token for UniTask
    private CancellationTokenSource cancellationTokenSource;

    // Background Color property
    private Color backgroundColor;
    public Color BackgroundColor
    {
        get { return backgroundColor; }
        set
        {
            backgroundColor = value;
            this.color = backgroundColor; // Update the Image color
        }
    }

    protected override void Awake()
    {
        base.Awake();
        this.material = buttonMaterial ? buttonMaterial : this.material;
        this.color = buttonColor;
        this.raycastTarget = raycastTarget;
        originalScale = transform.localScale; // Store the original scale
        backgroundColor = this.color; // Initialize backgroundColor

        cancellationTokenSource = new CancellationTokenSource();
    }

    private void OnEnable()
    {
        cancellationTokenSource = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        cancellationTokenSource.Cancel(); // Cancel all running UniTasks
        cancellationTokenSource.Dispose();
    }

    private void OnDestroy()
    {
        cancellationTokenSource.Cancel(); // Also cancel UniTasks on destruction
        cancellationTokenSource.Dispose();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        onPress?.Invoke();
        ScaleTo(originalScale * scaleDownFactor, animationDuration, false, cancellationTokenSource.Token).Forget(); // Scale down with UniTask
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        onReleased?.Invoke();
        ScaleTo(originalScale, animationDuration, true, cancellationTokenSource.Token).Forget(); // Scale back to original with UniTask
    }

    private async UniTaskVoid ScaleTo(Vector3 targetScale, float duration, bool setOnComplete, CancellationToken cancellationToken)
    {
        Vector3 startScale = transform.localScale;
        float time = 0;

        while (time < duration)
        {
            if (cancellationToken.IsCancellationRequested) return;

            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime;
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
        }

        if (setOnComplete && !cancellationToken.IsCancellationRequested)
            transform.localScale = targetScale; // Ensure the target scale is set precisely at the end
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        if (!raycastTarget) return false;

        return RectTransformUtility.RectangleContainsScreenPoint(
            rectTransform,
            screenPoint,
            eventCamera,
            raycastPadding
        );
    }
}