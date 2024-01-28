using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PopUpActivator : MonoBehaviour
{
    [SerializeField] List<GameObject> popUpList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomSelection()
    {
        GameObject selectedSprite = popUpList[Random.Range(0, popUpList.Count)];
        selectedSprite.SetActive(true);
    }
}
