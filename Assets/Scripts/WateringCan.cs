using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    [SerializeField] private float rotThreshold = 25f;
    [SerializeField] private Transform waterSpawnLocation;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private int spawnNum = 1;
    [SerializeField] private float dirRandomRange = 0.15f;
    [SerializeField] private float waterForce = 0.5f;
    private float timer = 0f;
    private StatTracker ST;
    private AudioSource AS;
    public float waterMaxVol = 1f;
    private float waterCurVol;
    public float waterVolRate = 1.75f;
    private bool isTipped = false;
    private Vector3 baseScale;
    private Vector3 socketScale;

    private void Start()
    {
        ST = GameObject.FindAnyObjectByType<StatTracker>();
        waterCurVol = 0f;
        AS = GetComponent<AudioSource>();
        baseScale = transform.localScale;
        socketScale = baseScale * .5f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GetStats();
        if (CheckRotation())
        {
            SpawnWater();
        }
        
    }

    private void Update()
    {
        ChangeVol();
        CheckForSocket();
    }

    private void GetStats()
    {
        spawnNum = ST.waterCanDropAmount;
        fireRate = ST.waterCanFireRate;
        waterForce = ST.waterForce;
    }

    private bool CheckRotation()
    {
        float xRot = transform.localEulerAngles.x;

        // Normalize angle to -180 to 180
        if (xRot > 180f)
            xRot -= 360f;

        //Debug.Log(xRot);

        if (xRot >= rotThreshold)
        {
            isTipped = true;
            return true;
        }
        timer = 0f;
        isTipped = false;
        return false;
    }

    private void SpawnWater()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            for (int i = 0; i < spawnNum; i++)
            {
                GameObject drop = Instantiate(waterPrefab, waterSpawnLocation.position, waterSpawnLocation.rotation);
                Rigidbody RB = drop.GetComponent<Rigidbody>();

                //get random force dir
                float dirX = Random.Range(-dirRandomRange, dirRandomRange);
                float dirY = Random.Range(-dirRandomRange, dirRandomRange);

                // Start with forward direction
                Vector3 baseDir = waterSpawnLocation.forward;

                // Add slight variation in right and up directions
                Vector3 variedDir = baseDir + waterSpawnLocation.right * dirX + waterSpawnLocation.up * dirY;

                // Normalize to maintain consistent force
                Vector3 finalForce = variedDir.normalized * waterForce;

                RB.AddForce(finalForce, ForceMode.Impulse); // optional: use Impulse for instant push
            }
        }
    }

    private void ChangeVol()
    {
        Debug.Log("isTipped = " + isTipped);
        if (isTipped)
        {
            waterCurVol += Time.deltaTime * waterVolRate;
        } else
        {
            waterCurVol -= Time.deltaTime * waterVolRate;
        }
        waterCurVol = Mathf.Clamp(waterCurVol, 0f, 1f);

        AS.volume = waterCurVol;
    }

    private void CheckForSocket()
    {
        bool hasSocket = false;
        foreach (Transform child in transform)
        {
            //Debug.Log(child.name);
            if (child.name.Contains("Socket"))
            {
                //Debug.Log("Found Socket in gun transform");
                hasSocket = true;
            }
        }

        if (hasSocket)
        {
            transform.localScale = socketScale;
        } else
        {
            transform.localScale = baseScale;
        }
    }
}
