using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudBehavior : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlantData myparent;

    // Start is called before the first frame update
    void Start()
    {
        myparent = transform.parent.parent.parent.GetComponent<PlantData>();
        if (myparent != null)
        {
            Debug.Log("PLANTS ARENT WORKING. RIOT.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myparent.currentStage >= 3)
        {
            anim.SetTrigger("Bloom");
        }
    }
}
