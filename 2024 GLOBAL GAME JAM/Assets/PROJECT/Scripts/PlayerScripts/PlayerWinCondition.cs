using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinCondition : MonoBehaviour
{
    [SerializeField] float  Y_WinThreshold;
    public static Action IdiotHasWon;
    [SerializeField] AnimationClip winAnimation;
    public Animator playerAnimator;
    bool wonGame = false;



    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= Y_WinThreshold)
        {
            if (!wonGame)
            {
                playerAnimator.Play(winAnimation.name);
                wonGame = true;
            }
            IdiotHasWon?.Invoke();
        }
    }
}
