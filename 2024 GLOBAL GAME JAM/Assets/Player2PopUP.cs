using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2PopUP : MonoBehaviour
{
    [SerializeField] List<GameObject> popUpList;

    public void RandomSelection()
    {
        GameObject selectedSprite = popUpList[Random.Range(0, popUpList.Count)];
        selectedSprite.SetActive(true);
    }
}
