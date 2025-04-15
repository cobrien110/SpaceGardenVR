using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    [SerializeField] private float rotThreshold = 25f;
    [SerializeField] private Transform waterSpawnLocation;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private float fireRate = 0.1f;
    private float timer = 0f;
    private bool isRotated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckRotation())
        {
            SpawnWater();
        }
    }

    private bool CheckRotation()
    {
        if (transform.rotation.eulerAngles.x >= rotThreshold)
        {
            return true;
        }
        timer = 0f;
        return false;
    }

    private void SpawnWater()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            Instantiate(waterPrefab, waterSpawnLocation.position, Quaternion.identity);
        }
    }
}
