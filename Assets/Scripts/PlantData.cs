using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantData : MonoBehaviour
{
    [Header("Stage Prefabs (1 to 4)")]
    public GameObject[] stagePrefabs = new GameObject[4];

    [Header("Leaf Marker Empties for Each Stage")]
    public GameObject[] leafEmptiesStage1;
    public GameObject[] leafEmptiesStage2;
    public GameObject[] leafEmptiesStage3;
    public GameObject[] leafEmptiesStage4;

    [Header("Flower Marker Empties for Each Stage")]
    public GameObject[] flowerEmptiesStage1;
    public GameObject[] flowerEmptiesStage2;
    public GameObject[] flowerEmptiesStage3;
    public GameObject[] flowerEmptiesStage4;

    [Header("Prefabs")]
    public GameObject leafPrefab;
    public GameObject flowerPrefab;

    [Header("Materials")]
    public Material customPlantMaterial;
    public Material customLeafMaterial;
    public Material customFlowerMaterial;

    private int currentStage = 0; //Starts at 0 (Stage 1)
    private GameObject currentPlant;

    public void Start()
    {
        SpawnStage(currentStage);
    }

    public void AdvanceStage()
    {
        if (currentStage >= stagePrefabs.Length - 1)
        {
            Debug.Log("Already at max stage.");
            return;
        }

        currentStage++;
        SpawnStage(currentStage);
    }

    private void SpawnStage(int stageIndex)
    {
        if (currentPlant != null)
            Destroy(currentPlant);

        //Instantiate new stage
        currentPlant = Instantiate(stagePrefabs[stageIndex], transform.position, transform.rotation, transform);

        //Assign material to plant
        AssignMaterial(currentPlant, customPlantMaterial);

        //Spawn leaves and flowers
        GameObject[] leafEmpties = GetLeafEmpties(stageIndex);
        GameObject[] flowerEmpties = GetFlowerEmpties(stageIndex);

        foreach (GameObject empty in leafEmpties)
        {
            GameObject leaf = Instantiate(leafPrefab, empty.transform.position, empty.transform.rotation, currentPlant.transform);
            AssignMaterial(leaf, customLeafMaterial);
        }

        foreach (GameObject empty in flowerEmpties)
        {
            GameObject flower = Instantiate(flowerPrefab, empty.transform.position, empty.transform.rotation, currentPlant.transform);
            AssignMaterial(flower, customFlowerMaterial);
        }
    }

    private void AssignMaterial(GameObject obj, Material mat)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
            renderer.material = mat;

        foreach (Renderer childRenderer in obj.GetComponentsInChildren<Renderer>())
        {
            childRenderer.material = mat;
        }
    }

    private GameObject[] GetLeafEmpties(int stage)
    {
        return stage switch
        {
            0 => leafEmptiesStage1,
            1 => leafEmptiesStage2,
            2 => leafEmptiesStage3,
            3 => leafEmptiesStage4,
            _ => new GameObject[0],
        };
    }

    private GameObject[] GetFlowerEmpties(int stage)
    {
        return stage switch
        {
            0 => flowerEmptiesStage1,
            1 => flowerEmptiesStage2,
            2 => flowerEmptiesStage3,
            3 => flowerEmptiesStage4,
            _ => new GameObject[0],
        };
    }
}
