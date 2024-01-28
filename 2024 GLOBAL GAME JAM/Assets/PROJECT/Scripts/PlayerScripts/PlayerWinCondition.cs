using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinCondition : MonoBehaviour
{
    [SerializeField] float  Y_WinThreshold;
    public static Action IdiotHasWon;

   

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= Y_WinThreshold)
        {
            IdiotHasWon?.Invoke();
        }
    }
}
