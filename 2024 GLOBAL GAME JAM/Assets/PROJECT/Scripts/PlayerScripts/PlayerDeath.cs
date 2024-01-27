using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] GameObject player => FindObjectOfType<PlayerClimbing>().gameObject;
    [SerializeField] float Y_Threshold;
    [SerializeField] float delayToDisable;
    public static Action pleaseYouShouldDie;
    bool isKilled;

    private void Update()
    {
        if (player.transform.position.y <= Y_Threshold)
        {
            if(isKilled) return;
            pleaseYouShouldDie?.Invoke();
            isKilled = true;
            Invoke(nameof(GoPleaseDie), delayToDisable);
        }
    }

    void GoPleaseDie()
    {
        player.SetActive(false);
    }

}
