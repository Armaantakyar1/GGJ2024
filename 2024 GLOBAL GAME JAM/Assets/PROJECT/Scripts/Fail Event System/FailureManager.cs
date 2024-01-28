using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FailureManager : MonoBehaviour
{
    [SerializeField] List<Punisments> punismentList;
    [SerializeField] List<Punisments> towpunish;
    [SerializeField] GameObject position1;
    [SerializeField] GameObject position2;
    [SerializeField] bool player1GettingPunished;
    [SerializeField] bool player2GettingPunished;
    [SerializeField] string player1;
    [SerializeField] string player2;

    private void OnEnable() => PlayerInput.FailedKeyPressed += BeginPunishment;

    private void OnDisable() => PlayerInput.FailedKeyPressed -= BeginPunishment;

    public void BeginPunishment(string whichPlayer)
    {
        if (!player1GettingPunished && whichPlayer == player1)
        {
            player1GettingPunished = true;
            StartCoroutine(Player1PunishmentSelector());
        }
        if (!player2GettingPunished && whichPlayer == player2)
        {
            player1GettingPunished = true;
            StartCoroutine(Player2PunishmentSelector());
        }
    }


    IEnumerator Player1PunishmentSelector()
    {
        Punisments punismentsType = punismentList[Random.Range(0, punismentList.Count)];
        Instantiate(punismentsType.Prefab);
        //punismentsType.Prefab.transform.position = position1.transform.position;

        yield return new WaitForSeconds(punismentsType.clip.length);
        player1GettingPunished = false;
    }
    IEnumerator Player2PunishmentSelector()
    {
        Punisments punismentsType = towpunish[Random.Range(0, towpunish.Count)];
        Instantiate(punismentsType.Prefab);
        //punismentsType.Prefab.transform.position = position2.transform.position;
        yield return new WaitForSeconds(punismentsType.clip.length);
        player2GettingPunished = false;
    }
}