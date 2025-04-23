using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SellGun : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private LayerMask shootLayer;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private GameObject laser;

    // Start is called before the first frame update
    void Start()
    {
        SetLaser(false);   
    }

    private void Update()
    {
        CheckForSocket();
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
                Destroy(s.gameObject);
            }
        } else
        {
            Debug.Log("Hit no sellable objects");
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
