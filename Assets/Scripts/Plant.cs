using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Plant : MonoBehaviour
{
    private Rigidbody RB;
    private XRGrabInteractable XRGrab;
    [SerializeField] private bool isGrowing = false;
    private Collider col;
    [SerializeField] private int water = 0;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        XRGrab = GetComponent<XRGrabInteractable>();
        col = GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrowing)
        {
            transform.position = transform.parent.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Bury into pot
        if (other.tag.Equals("Dirt"))
        {
            Pot p = other.GetComponent<Pot>();
            if (p != null && !p.GetPlant())
            {
                p.SetPlant(this);

                isGrowing = true;
                transform.parent = p.seedGrowPoint;
                transform.position = transform.parent.position;

                RB.isKinematic = false;
                RB.velocity = Vector3.zero;
                RB.useGravity = false;
                //col.enabled = false;

                XRGrab.enabled = false;
            }
        }
    }

    public void Water(int amount)
    {
        Debug.Log("Adding water, " + name + " now has " + water + " water");
        water += amount;
    }
}
