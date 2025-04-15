using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Plant : MonoBehaviour
{
    private Rigidbody RB;
    private XRGrabInteractable XRGrab;
    [SerializeField] private bool isGrowing = false;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        XRGrab = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (XRGrab != null && isGrowing && XRGrab.enabled)
        {
            XRGrab.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Bury into pot
        if (other.tag.Equals("Dirt"))
        {
            isGrowing = true;
            Pot p = other.GetComponentInParent<Pot>();
            transform.position = p.seedGrowPoint.position;
            RB.velocity = Vector3.zero;
        }
    }
}
