using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class PlantConstructor : MonoBehaviour
{
    /*
    [Header("Growth Parameters")]
    [Range(0f, 1f)] public float growthProgress = 0f;
    public float maxHeight = 5f;
    public int maxBranches = 5;
    public float branchFrequency = 0.3f;
    public float leafFrequency = 0.5f;

    [Header("Visual References")]
    public Material stalkMaterial;
    public GameObject leafPrefab;
    public GameObject budPrefab;

    [Header("Spline Settings")]
    public float stalkRadius = 0.1f;

    private SplineExtrude stalkExtrude;
    private GameObject stalkObject;

    [Header("Debug View")]
    public Material lineMaterial;
    public int splineResolution = 20;

    private LineRenderer stalkLineRenderer;

    private PlantStructure structure;
    private GameObject plantRoot;
    private SplineContainer mainSpline;

    void Start()
    {
        GeneratePlantStructure();
        BuildSplineStructure();
        SetGrowth(0f);
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            float t = Mathf.PingPong(Time.time * 0.1f, 1f);
            SetGrowth(t);
        }
    }

    public void GeneratePlantStructure()
    {
        structure = new PlantStructure();
        structure.height = maxHeight;

        int segments = Mathf.CeilToInt(maxHeight);
        for (int i = 0; i < segments; i++)
        {
            float heightRatio = (i + 1) / (float)segments;

            if (Random.value < leafFrequency)
            {
                structure.leafPoints.Add(new PlantStructure.LeafPoint
                {
                    heightRatio = heightRatio
                });
            }

            if (Random.value < branchFrequency && structure.branches.Count < maxBranches)
            {
                structure.branches.Add(new PlantStructure.Branch
                {
                    heightRatio = heightRatio,
                    angle = Random.Range(-45f, 45f),
                    length = Random.Range(0.5f, 1.5f)
                });
            }
        }

        structure.budHeightRatio = 1f;
    }

    private void SetupStalkLineRenderer()
    {
        stalkLineRenderer = plantRoot.AddComponent<LineRenderer>();
        stalkLineRenderer.positionCount = splineResolution;
        stalkLineRenderer.material = lineMaterial;
        stalkLineRenderer.widthMultiplier = 0.05f;
        stalkLineRenderer.useWorldSpace = false;
    }


    public void BuildSplineStructure()
    {
        if (plantRoot != null)
            Destroy(plantRoot);

        plantRoot = new GameObject("Plant");
        plantRoot.transform.parent = transform;

        //Main stalk spline
        stalkObject = new GameObject("StalkSpline");
        stalkObject.transform.parent = plantRoot.transform;

        mainSpline = stalkObject.AddComponent<SplineContainer>();
        var spline = new Spline();
        spline.Add(new BezierKnot(Vector3.zero));
        spline.Add(new BezierKnot(Vector3.up * structure.height)); //Full height
        mainSpline.Spline = spline;

        //Extrude visual
        stalkExtrude = stalkObject.AddComponent<SplineExtrude>();
        stalkExtrude.extrudeMethod = SplineExtrude.ExtrudeMethod.Instance;
        stalkExtrude.extrude = true;
        stalkExtrude.radius = stalkRadius;

        if (stalkMaterial)
            stalkExtrude.material = stalkMaterial;

        UpdateStalk();
    }

    public void SetGrowth(float progress)
    {
        growthProgress = Mathf.Clamp01(progress);
        UpdateStalk();
        TrySpawnBranches();
    }

    private void SpawnBranch(Vector3 worldPos)
    {
        GameObject branch = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        branch.transform.position = worldPos;
        branch.transform.localScale = new Vector3(0.05f, 0.2f, 0.05f);
        branch.transform.parent = plantRoot.transform;
    }

    private void UpdateStalk()
    {
        if (mainSpline == null || structure == null)
            return;

        var spline = mainSpline.Spline;

        //Reset to two base knots
        spline.Clear();
        spline.Add(new BezierKnot(Vector3.zero));

        //Current growth height
        float currentHeight = structure.height * growthProgress;
        spline.Add(new BezierKnot(Vector3.up * currentHeight));

        //Let SplineExtrude re-extrude it
        stalkExtrude.Refresh();
    }

    private void UpdateBranches()
    {
        // WIP: Later we can create branch splines at specific knot heights
        // and grow them the same way as the main stalk.
    }

    private void UpdateLeavesAndBud()
    {
        // WIP: Place leaves and bud objects along the spline
    }*/
}
