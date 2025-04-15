using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public Transform seedGrowPoint;
    [SerializeField] private Plant myPlant;

    private void OnTriggerEnter(Collider other)
    {
        // add water to plant
        if (other.tag.Equals("Water"))
        {
            if (myPlant != null) myPlant.Water(1);
            Destroy(other.gameObject);
        }
    }
}
