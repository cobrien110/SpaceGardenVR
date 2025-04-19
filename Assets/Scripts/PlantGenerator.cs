using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class PlantGenerator : MonoBehaviour
{
    [Header("Growth Settings")]
    public float plantHeight = 5f;
    public int maxKnots = 10;
    public float growthSpeed = 0.2f;
    public float knotInterval = 0.1f;

    [Header("Visual Settings")]
    public float bendStrength = 0.5f;
    public float knotDistance = 0.5f;
    public Material stalkMaterial;

    private GameObject plantObject;
    private SplineContainer splineContainer;
    private SplineExtrude splineExtrude;
    private Spline spline;
    private MeshFilter meshFilter;

    private float growthProgress = 0f;
    private int knotsAdded = 1;

    void Start()
    {
        CreatePlant();
    }

    void Update()
    {
        if (growthProgress < 1f)
        {
            SetGrowth(growthProgress + Time.deltaTime * growthSpeed);
        }
    }

    public void CreatePlant()
    {
        if (plantObject != null)
            Destroy(plantObject);

        plantObject = new GameObject("GeneratedPlant");
        plantObject.transform.SetParent(transform, false);

        splineContainer = plantObject.AddComponent<SplineContainer>();
        splineExtrude = plantObject.AddComponent<SplineExtrude>();
        splineExtrude.Container = splineContainer;

        //ensure MeshFilter exists
        meshFilter = plantObject.GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = plantObject.AddComponent<MeshFilter>();

        //create a writable mesh and assign it
        Mesh generatedMesh = new Mesh();
        generatedMesh.name = "GeneratedPlantMesh";
        generatedMesh.MarkDynamic(); //helps performance for runtime updates
        meshFilter.sharedMesh = generatedMesh;

        //ensure MeshRenderer exists and apply material
        MeshRenderer meshRenderer = plantObject.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            meshRenderer = plantObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = stalkMaterial;

        //initialize spline with 2 knots
        spline = new Spline();
        Vector3 start = Vector3.zero;
        Vector3 first = Vector3.up * knotDistance;

        //downward tangent for the base
        Vector3 downTangent = Vector3.down * 0.1f;
        Vector3 upTangent = Vector3.up * 0.1f;

        spline.Add(new BezierKnot(start)
        {
            TangentIn = downTangent,
            TangentOut = upTangent
        });

        spline.Add(new BezierKnot(first)
        {
            TangentIn = -upTangent,
            TangentOut = upTangent
        });

        splineContainer.Spline = spline;

        growthProgress = 0f;
        knotsAdded = 1;

        //force initial refresh
        splineExtrude.Rebuild();
    }

    public void SetGrowth(float progress)
    {
        growthProgress = Mathf.Clamp01(progress);

        float targetHeight = plantHeight * growthProgress;

        if (spline.Count > 1)
        {
            var last = spline[spline.Count - 1];
            last.Position = new Vector3(last.Position.x, targetHeight, last.Position.z);
            spline[spline.Count - 1] = last;
        }

        if (growthProgress >= knotInterval * knotsAdded && spline.Count < maxKnots)
        {
            AddKnot();
            knotsAdded++;
        }

        //request rebuild
        splineExtrude.Rebuild();
    }

    private void AddKnot()
    {
        Vector3 lastPos = spline[spline.Count - 1].Position;
        Vector3 offset = Random.insideUnitSphere;
        offset.y = Mathf.Abs(offset.y);
        offset *= bendStrength;

        Vector3 nextPos = lastPos + offset + Vector3.up * knotDistance;

        //calculate tangent pointing in growth direction
        Vector3 forward = (nextPos - lastPos).normalized * 0.1f;

        BezierKnot newKnot = new BezierKnot(nextPos)
        {
            TangentIn = -forward,
            TangentOut = forward
        };

        spline.Add(newKnot);

    }
}