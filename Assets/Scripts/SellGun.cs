using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SellGun : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private LayerMask shootLayer;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private GameObject laser;
    [SerializeField] private Material[] laserMats;
    private AudioSource AS;
    [SerializeField] private TextMeshPro text;
    private Vector3 baseScale;
    private Vector3 socketScale;
    public float safeRange = 80f;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        SetLaser(false);
        AS = GetComponent<AudioSource>();
        baseScale = transform.localScale;
        socketScale = baseScale * .5f;
        startPos = transform.position;
    }

    private void Update()
    {
        CheckForSocket();
        CheckForPlantInRange();
        CheckDistance();
    }

    public void Shoot()
    {
        // Check to see plants within range that can be sold
        RaycastHit hit;
        if (Physics.Raycast(shootOrigin.position, shootOrigin.forward, out hit, range, shootLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit: " + hit.transform.gameObject.name);

            SellableObject s = hit.transform.GetComponent<SellableObject>();
            if (s != null)
            {
                s.Sell();
                AS.Play();
            }
        } else
        {
            Debug.Log("Hit no sellable objects");
        }
    }

    private void CheckForPlantInRange()
    {
        // Check to see plants within range that can be sold
        RaycastHit hit;
        if (Physics.Raycast(shootOrigin.position, shootOrigin.forward, out hit, range, shootLayer))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            SellableObject s = hit.transform.GetComponent<SellableObject>();
            if (s != null && s.GetIsSellable() && laser.activeInHierarchy)
            {
                laser.GetComponent<MeshRenderer>().material = laserMats[1];
                text.text = "$" + s.GetValue();
            } else if (laser.activeInHierarchy)
            {
                laser.GetComponent<MeshRenderer>().material = laserMats[0];
                text.text = "";
            }
        }
        else if (laser.activeInHierarchy)
        {
            laser.GetComponent<MeshRenderer>().material = laserMats[0];
            text.text = "";
        }
    }

    // turns laser
    public void SetLaser(bool isOn)
    {
        laser.SetActive(isOn);
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(transform.position, startPos) > safeRange)
        {
            transform.position = startPos;
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void CheckForSocket()
    {
        //if (!laser.activeInHierarchy) return;
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
            SetLaser(false);
        }
        else
        {
            transform.localScale = baseScale;
        }
    }
}
