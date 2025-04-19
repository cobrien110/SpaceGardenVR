using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class PlantGenerator : MonoBehaviour
{
    [Header("Growth Settings")]
    public float plantHeight = 5f;
    public int maxKnots = 10;
    public float growthSpeed = 0.2f; // Units per second
    public float knotInterval = 0.1f; // New knot every 10% of growth

    [Header("Spline Settings")]
    public float bendStrength = 0.5f;
    public float knotDistance = 0.5f;
    public Material stalkMaterial;

    private GameObject plantObject;
    private SplineContainer splineContainer;
    private Spline spline;
    private SplineExtrude extrude;

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
            float delta = Time.deltaTime * growthSpeed;
            SetGrowth(growthProgress + delta);
        }
    }

    public void CreatePlant()
    {
        if (plantObject != null)
            Destroy(plantObject);

        // Create and parent the plant object
        plantObject = new GameObject("GeneratedPlant");
        plantObject.transform.parent = transform;
        plantObject.transform.localPosition = Vector3.zero;

        // Add spline components
        splineContainer = plantObject.AddComponent<SplineContainer>();
        extrude = plantObject.AddComponent<SplineExtrude>();
        extrude.extrudeMethod = SplineExtrude.ExtrudeMethod.PerSegment;
        extrude.rebuildOnSplineChange = true;

        // Apply material if given
        if (stalkMaterial != null)
        {
            var meshRenderer = plantObject.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
                meshRenderer = plantObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = stalkMaterial;
        }

        // Initialize spline
        spline = new Spline();
        Vector3 basePos = Vector3.zero;
        spline.Add(new BezierKnot(basePos));
        spline.Add(new BezierKnot(basePos + Vector3.up * knotDistance));
        splineContainer.Spline = spline;

        knotsAdded = 1;
        growthProgress = 0f;
    }

    public void SetGrowth(float progress)
    {
        growthProgress = Mathf.Clamp01(progress);

        float currentHeight = plantHeight * growthProgress;

        // Move the last knot upwards
        if (spline.Count > 1)
        {
            BezierKnot last = spline[spline.Count - 1];
            last.Position = new Vector3(last.Position.x, currentHeight, last.Position.z);
            spline[spline.Count - 1] = last;
        }

        // Add a new knot at intervals
        if (growthProgress >= knotInterval * knotsAdded && spline.Count < maxKnots)
        {
            AddKnot();
            knotsAdded++;
        }

        // Force mesh update
        extrude.Refresh();
    }

    private void AddKnot()
    {
        Vector3 lastPos = spline[spline.Count - 1].Position;
        Vector3 offset = Random.insideUnitSphere;
        offset.y = Mathf.Abs(offset.y); // Bias toward upward
        offset *= bendStrength;

        Vector3 newPos = lastPos + offset + Vector3.up * knotDistance;
        spline.Add(new BezierKnot(newPos));
    }
}