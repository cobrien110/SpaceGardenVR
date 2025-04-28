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

    public void SetStage(int num)
    {
        if (num == currentStage) return;
        currentStage = num;
        if (currentStage > stagePrefabs.Length - 1)
        {
            Debug.Log("Already at max stage.");
            return;
        }
        Debug.Log("This gets called");
        SpawnStage(num);
    }

    private void SpawnStage(int stageIndex)
    {
        if (currentPlant != null)
            Destroy(currentPlant);

        //Instantiate new stage
        currentPlant = Instantiate(stagePrefabs[stageIndex], transform.position, transform.rotation, transform);
        
        //Auto-assign empties by tag from each stage prefab
        GameObject[] leafEmpties = FindTaggedEmpties(gameObject, "LeafEmpty");

        GameObject[] flowerEmpties = FindTaggedEmpties(gameObject, "FlowerEmpty");
        //Assign material to plant
        AssignMaterial(currentPlant, customPlantMaterial);

        //Spawn leaves and flowers

        foreach (GameObject empty in leafEmpties)
        {
            GameObject leaf = Instantiate(leafPrefab, empty.transform.localPosition, empty.transform.localRotation, currentPlant.transform);
            leaf.transform.localScale = empty.transform.localScale;
            AssignMaterial(leaf, customLeafMaterial);
        }

        foreach (GameObject empty in flowerEmpties)
        {
            GameObject flower = Instantiate(flowerPrefab, empty.transform.localPosition, empty.transform.localRotation, currentPlant.transform);
            Debug.Log(gameObject);
            flower.transform.localScale = empty.transform.localScale;
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
    private GameObject[] FindTaggedEmpties(GameObject plantPrefab, string tag)
    {
        Transform[] allChildren = plantPrefab.GetComponentsInChildren<Transform>(true);
        List<GameObject> taggedEmpties = new();

        foreach (Transform child in allChildren)
        {
            if (child.CompareTag(tag))
            {

                taggedEmpties.Add(child.gameObject);
            }

        }

        return taggedEmpties.ToArray();
    }
}
