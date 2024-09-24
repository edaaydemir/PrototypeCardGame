using TMPro;
using UnityEngine;

public class TextView : MonoBehaviour
{
    public TMP_Text tmpText;

    public void Update()
    {
        if (tmpText != null)
        {
            string currentPlayerTurnText = GameManager.Instance.WhoIsPlaying == PlayerType.Player ? "Player Turn" : "Enemy Turn";
            tmpText.SetText(currentPlayerTurnText);
        }
    }
}
