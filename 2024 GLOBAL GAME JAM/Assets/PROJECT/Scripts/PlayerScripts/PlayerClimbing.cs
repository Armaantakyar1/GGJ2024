using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;


public class PlayerClimbing : MonoBehaviour
{

    [SerializeField] float duration;
    [SerializeField] AnimationCurve moveCurve;
    [SerializeField] Vector2 desiredPosition;
    [SerializeField] float correctOffset;
    [SerializeField] float wrongOffset;

    float timer;
    Vector2 previousDesiredPosition;
    bool timerReset;

    private void Start()
    {
        previousDesiredPosition = transform.position;
        desiredPosition = transform.position;
    }


    public void ClimbUp()
    {
        desiredPosition = new Vector2(desiredPosition.x, desiredPosition.y + correctOffset);
    }

    public void ClimbDown()
    {
        desiredPosition = new Vector2(desiredPosition.x, desiredPosition.y - correctOffset);
    }


    private void Update()
    {
        if (desiredPosition == previousDesiredPosition && !timerReset) return;
        else if(desiredPosition != previousDesiredPosition && timerReset)
        {timer = 0;timerReset = true;}

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
