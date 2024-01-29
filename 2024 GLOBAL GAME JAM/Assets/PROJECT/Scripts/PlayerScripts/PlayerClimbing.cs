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
    [SerializeField] Vector3 desiredPosition;
    [SerializeField] float correctOffset;
    [SerializeField] float wrongOffset;
    [SerializeField] string playerType;
    

    [Tab("Debug Info")]
    [SerializeField] float timer;
    [SerializeField] Vector3 previousDesiredPosition;
    [SerializeField] bool timerReset;

    [Tab("Animation Info")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] AnimationClip idleClip;
    [SerializeField] AnimationClip climbClip;
    [SerializeField] AnimationClip[] bonks;

    private void Start()
    {
        previousDesiredPosition = transform.position;
        desiredPosition = transform.position;
    }

    private void OnEnable()
    {
        PlayerInput.SuccessBitch += ClimbUp; 
        KIllPunishment.GetBonkedBitch += ClimbDown;
    }

    private void OnDisable()
    {
        PlayerInput.SuccessBitch -= ClimbUp;
        KIllPunishment.GetBonkedBitch -= ClimbDown;
    }

    [ContextMenu("Climb Up Bitch")]
    public void ClimbUp(string player)
    {
        if (player != playerType) return;
        desiredPosition = new Vector3(desiredPosition.x, desiredPosition.y + correctOffset,transform.position.z);
        playerAnimator.Play(climbClip.name);
    }

    [ContextMenu("Get Bonked Bitch")]
    public void ClimbDown(GameObject player)
    {
        if (player != gameObject) return;
        desiredPosition = new Vector3(desiredPosition.x, desiredPosition.y - correctOffset,transform.position.z);
        playerAnimator.Play(bonks[UnityEngine.Random.Range(0, bonks.Length)].name);
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
            transform.position = Vector3.Lerp(previousDesiredPosition, desiredPosition, t);
        }
    }
}
