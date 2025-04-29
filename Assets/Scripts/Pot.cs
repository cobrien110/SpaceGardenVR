using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public Transform seedGrowPoint;
    [SerializeField] private Plant myPlant;
    public ChomperPlant Chomp;
    public GameObject parentObject;

    private void OnTriggerEnter(Collider other)
    {
        // add water to plant
        if (other.tag.Equals("Water"))
        {
            if (myPlant != null) myPlant.Water(1);
            Destroy(other.gameObject);
        }
    }

    public bool GetPlant()
    {
        if (myPlant != null) return true;
        return false;
    }

    public void SetPlant(Plant plant)
    {
        myPlant = plant;
    }

    public void Start()
    {
        Chomp = GameObject.FindAnyObjectByType<ChomperPlant>();
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, Chomp.mouthPoint.transform.position) < Chomp.rangeChomperEat && myPlant != null)
        {
            myPlant.FeedToChomper();
            Destroy(parentObject);
        }
    }
}
