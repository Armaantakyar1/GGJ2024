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
        if ( whichPlayer == player1)
        {
            player1GettingPunished = true;
            StartCoroutine(PlayerPunishmentSelector(position1.transform.position));
        }
        if (whichPlayer == player2)
        {
            player1GettingPunished = true;
            StartCoroutine(PlayerPunishmentSelector(position2.transform.position));
        }
    }


    IEnumerator PlayerPunishmentSelector(Vector3 playerPosition)
    {
        Punisments punismentsType = punismentList[Random.Range(0, punismentList.Count)];
        Instantiate(punismentsType.Prefab,playerPosition, punismentsType.Prefab.transform.rotation);

        yield return new WaitForSeconds(1.5f);
    }
  
}