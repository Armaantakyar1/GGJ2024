using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PopUpActivator : MonoBehaviour
{
    [SerializeField] List<GameObject> popUpList;

    private void OnEnable()
    {
        KIllPunishment.GetBonkedBitch += RandomSelection;
    }
    private void OnDisable()
    {
        KIllPunishment.GetBonkedBitch -= RandomSelection;
    }

    public void RandomSelection(GameObject player)
    {
        if(player!=gameObject) return;
        GameObject selectedSprite = popUpList[Random.Range(0, popUpList.Count)];
        selectedSprite.SetActive(true);
    }
}
