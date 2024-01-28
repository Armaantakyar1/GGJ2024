using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAction : MonoBehaviour
{
    [SerializeField] GameObject itself;
    [SerializeField] PopUpActivator pop;
    // Start is called before the first frame update
    void Start()
    {
        pop = FindObjectOfType<PopUpActivator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopUp()
    {
        pop.RandomSelection();
    }

    public void RemoveObject()
    {
        Destroy(itself);
    }

    public void DisableObject()
    {
        itself.SetActive(false);
    }

}
