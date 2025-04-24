using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFormation : MonoBehaviour
{
    [Header("Plant Stage Prefabs by Type")]
    public GameObject[] plantType0Stages = new GameObject[4];
    public GameObject[] plantType1Stages = new GameObject[4];
    public GameObject[] plantType2Stages = new GameObject[4];

    [Header("Leaf and Flower Prefabs")]
    public GameObject[] leafTypes;
    public GameObject[] flowerTypes;

    [Header("Texture Maps")]
    public Texture2D[] detailMaps;
    public Texture2D[] colorMaps;

    [Header("Base Materials")]
    public Material basePlantMaterial;
    public Material baseLeafMaterial;
    public Material baseFlowerMaterial;

    public GameObject ConstructPlant()
    {
        int plantTypeIndex = Random.Range(0, 3);
        int leafIndex = Random.Range(0, leafTypes.Length);
        int flowerIndex = Random.Range(0, flowerTypes.Length);

        GameObject[] selectedStages = GetStages(plantTypeIndex);
        GameObject seedling = Instantiate(selectedStages[0], transform.position, Quaternion.identity);
        PlantData data = seedling.AddComponent<PlantData>();
        data.stagePrefabs = selectedStages;

        //Auto-assign empties by tag from each stage prefab
        data.leafEmptiesStage1 = FindTaggedEmpties(selectedStages[0], "LeafEmpty");
        data.leafEmptiesStage2 = FindTaggedEmpties(selectedStages[1], "LeafEmpty");
        data.leafEmptiesStage3 = FindTaggedEmpties(selectedStages[2], "LeafEmpty");
        data.leafEmptiesStage4 = FindTaggedEmpties(selectedStages[3], "LeafEmpty");

        data.flowerEmptiesStage1 = FindTaggedEmpties(selectedStages[0], "FlowerEmpty");
        data.flowerEmptiesStage2 = FindTaggedEmpties(selectedStages[1], "FlowerEmpty");
        data.flowerEmptiesStage3 = FindTaggedEmpties(selectedStages[2], "FlowerEmpty");
        data.flowerEmptiesStage4 = FindTaggedEmpties(selectedStages[3], "FlowerEmpty");

        data.leafPrefab = leafTypes[leafIndex];
        data.flowerPrefab = flowerTypes[flowerIndex];

        data.customPlantMaterial = MakeMaterial(0);
        data.customLeafMaterial = MakeMaterial(1);
        data.customFlowerMaterial = MakeMaterial(2);

        return seedling;
    }

    private GameObject[] GetStages(int type)
    {
        return type switch
        {
            0 => plantType0Stages,
            1 => plantType1Stages,
            2 => plantType2Stages,
            _ => new GameObject[4],
        };
    }

    private GameObject[] FindTaggedEmpties(GameObject plantPrefab, string tag)
    {
        Transform[] allChildren = plantPrefab.GetComponentsInChildren<Transform>(true);
        List<GameObject> taggedEmpties = new();

        foreach (Transform child in allChildren)
        {
            if (child.CompareTag(tag))
                taggedEmpties.Add(child.gameObject);
        }

        return taggedEmpties.ToArray();
    }

    private Material MakeMaterial(int type)
    {
        Texture2D detailMap = detailMaps[Random.Range(0, detailMaps.Length)];
        Texture2D colorMap = colorMaps[Random.Range(0, colorMaps.Length)];
        Vector2 coord = new Vector2(Random.Range(0, 2048), Random.Range(0, 2048));

        Material newMat = type switch
        {
            0 => new Material(basePlantMaterial),
            1 => new Material(baseLeafMaterial),
            2 => new Material(baseFlowerMaterial),
            _ => new Material(basePlantMaterial),
        };

        //if only one color type
        colorMap = colorMaps[type];

        newMat.SetTexture("_DetailMap", detailMap);
        newMat.SetTexture("_ColorMap", colorMap);
        newMat.SetVector("_ColorCoordinate", coord);

        return newMat;
    }
}