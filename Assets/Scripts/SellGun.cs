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

    // Start is called before the first frame update
    void Start()
    {
        SetLaser(false);
        AS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckForSocket();
        CheckForPlantInRange();
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

    private void CheckForSocket()
    {
        if (!laser.activeInHierarchy) return;
        foreach (Transform child in transform)
        {
            //Debug.Log(child.name);
            if (child.name.Contains("Socket"))
            {
                //Debug.Log("Found Socket in gun transform");
                SetLaser(false);
                return;
            }
        }
    }
}
