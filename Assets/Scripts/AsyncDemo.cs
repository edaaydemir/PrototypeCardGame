using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AsyncDemo : MonoBehaviour
{
    private int maxCount = 10;
    
    private async void Start()
    {
        Debug.Log("start event");
        await CountDown();
        Debug.Log("end event");
    }
    
    
    private async UniTask CountDown()
    {
        while (maxCount > 0)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            Debug.Log("Loop " + maxCount);
            maxCount--;
        }
    }
}
