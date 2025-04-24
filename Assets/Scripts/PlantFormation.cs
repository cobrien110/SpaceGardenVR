using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFormation : MonoBehaviour
{
    [Header("Plant Stage Prefabs by Type")]
    public GameObject[] plantType0Stages = new GameObject[4];
    public GameObject[] plantType1Stages = new GameObject[4];
    public GameObject[] plantType2Stages = new GameObject[4];

    [Header("Leaf Empties by Type and Stage")]
    public GameObject[] leafEmptiesType0Stage1;
    public GameObject[] leafEmptiesType0Stage2;
    public GameObject[] leafEmptiesType0Stage3;
    public GameObject[] leafEmptiesType0Stage4;

    public GameObject[] leafEmptiesType1Stage1;
    public GameObject[] leafEmptiesType1Stage2;
    public GameObject[] leafEmptiesType1Stage3;
    public GameObject[] leafEmptiesType1Stage4;

    public GameObject[] leafEmptiesType2Stage1;
    public GameObject[] leafEmptiesType2Stage2;
    public GameObject[] leafEmptiesType2Stage3;
    public GameObject[] leafEmptiesType2Stage4;

    [Header("Flower Empties by Type and Stage")]
    public GameObject[] flowerEmptiesType0Stage1;
    public GameObject[] flowerEmptiesType0Stage2;
    public GameObject[] flowerEmptiesType0Stage3;
    public GameObject[] flowerEmptiesType0Stage4;

    public GameObject[] flowerEmptiesType1Stage1;
    public GameObject[] flowerEmptiesType1Stage2;
    public GameObject[] flowerEmptiesType1Stage3;
    public GameObject[] flowerEmptiesType1Stage4;

    public GameObject[] flowerEmptiesType2Stage1;
    public GameObject[] flowerEmptiesType2Stage2;
    public GameObject[] flowerEmptiesType2Stage3;
    public GameObject[] flowerEmptiesType2Stage4;

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
        AssignEmpties(data, plantTypeIndex);
        data.leafPrefab = leafTypes[leafIndex];
        data.flowerPrefab = flowerTypes[flowerIndex];

        data.customPlantMaterial = MakeMaterial(0);
        data.customLeafMaterial = MakeMaterial(1);
        data.customFlowerMaterial = MakeMaterial(2);

        return seedling;
    }

    private void AssignEmpties(PlantData data, int type)
    {
        switch (type)
        {
            case 0:
                data.leafEmptiesStage1 = leafEmptiesType0Stage1;
                data.leafEmptiesStage2 = leafEmptiesType0Stage2;
                data.leafEmptiesStage3 = leafEmptiesType0Stage3;
                data.leafEmptiesStage4 = leafEmptiesType0Stage4;

                data.flowerEmptiesStage1 = flowerEmptiesType0Stage1;
                data.flowerEmptiesStage2 = flowerEmptiesType0Stage2;
                data.flowerEmptiesStage3 = flowerEmptiesType0Stage3;
                data.flowerEmptiesStage4 = flowerEmptiesType0Stage4;
                break;

            case 1:
                data.leafEmptiesStage1 = leafEmptiesType1Stage1;
                data.leafEmptiesStage2 = leafEmptiesType1Stage2;
                data.leafEmptiesStage3 = leafEmptiesType1Stage3;
                data.leafEmptiesStage4 = leafEmptiesType1Stage4;

                data.flowerEmptiesStage1 = flowerEmptiesType1Stage1;
                data.flowerEmptiesStage2 = flowerEmptiesType1Stage2;
                data.flowerEmptiesStage3 = flowerEmptiesType1Stage3;
                data.flowerEmptiesStage4 = flowerEmptiesType1Stage4;
                break;

            case 2:
                data.leafEmptiesStage1 = leafEmptiesType2Stage1;
                data.leafEmptiesStage2 = leafEmptiesType2Stage2;
                data.leafEmptiesStage3 = leafEmptiesType2Stage3;
                data.leafEmptiesStage4 = leafEmptiesType2Stage4;

                data.flowerEmptiesStage1 = flowerEmptiesType2Stage1;
                data.flowerEmptiesStage2 = flowerEmptiesType2Stage2;
                data.flowerEmptiesStage3 = flowerEmptiesType2Stage3;
                data.flowerEmptiesStage4 = flowerEmptiesType2Stage4;
                break;
        }
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

        newMat.SetTexture("_DetailMap", detailMap);
        newMat.SetTexture("_ColorMap", colorMap);
        newMat.SetVector("_ColorCoordinate", coord);

        return newMat;
    }
}