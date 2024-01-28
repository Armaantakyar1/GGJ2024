using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2AnimationAction : MonoBehaviour
{
    [SerializeField] GameObject itself;
    [SerializeField] Player2PopUP secondpop;

    void Start()
    {
        secondpop = FindObjectOfType<Player2PopUP>();
    }

    public void PopUp()
    {
        secondpop.RandomSelection();
    }

    public void RemoveObject()
    {
        Destroy(itself);
    }

    public void DisableObject(GameObject themself)
    {
        themself.SetActive(false);
    }
}
