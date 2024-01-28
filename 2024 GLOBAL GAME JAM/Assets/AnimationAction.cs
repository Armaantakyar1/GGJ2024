using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAction : MonoBehaviour
{
    [SerializeField] GameObject itself;
    [SerializeField] PopUpActivator pop;

    void Start()
    {
        pop = FindObjectOfType<PopUpActivator>();
    }

    public void PopUp()
    {
        pop.RandomSelection();
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
