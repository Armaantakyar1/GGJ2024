using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using VInspector;


public class PlayerClimbing : MonoBehaviour
{
    [Tab("Movement Info")]
    [SerializeField] float duration;
    [SerializeField] AnimationCurve moveCurve;
    [SerializeField] Vector2 desiredPosition;
    [SerializeField] float correctOffset;
    [SerializeField] float wrongOffset;
    

    [Tab("Debug Info")]
    [SerializeField] float timer;
    [SerializeField] Vector2 previousDesiredPosition;
    [SerializeField] bool timerReset;

    [Tab("Animation Info")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] AnimationClip idleClip;
    [SerializeField] AnimationClip climbClip;

    private void Start()
    {
        previousDesiredPosition = transform.position;
        desiredPosition = transform.position;
    }

    [ContextMenu("Climb Up Bitch")]
    public void ClimbUp()
    {
        desiredPosition = new Vector2(desiredPosition.x, desiredPosition.y + correctOffset);
        playerAnimator.Play(climbClip.name);
    }

    [ContextMenu("Get Bonked Bitch")]
    public void ClimbDown()
    {
        desiredPosition = new Vector2(desiredPosition.x, desiredPosition.y - correctOffset);
        playerAnimator.Play(idleClip.name);
    }

    private void Update()
    {
        if (desiredPosition == previousDesiredPosition && !timerReset)
        { playerAnimator.Play(idleClip.name); return; }
        else if (desiredPosition != previousDesiredPosition && !timerReset)
        { timer = 0; timerReset = true; }

        if(timer >= duration)
        {
            previousDesiredPosition = desiredPosition;
            timerReset = false;
        }

        if(timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            t = moveCurve.Evaluate(t);
            transform.position = Vector2.Lerp(previousDesiredPosition, desiredPosition, t);
        }
    }
}
