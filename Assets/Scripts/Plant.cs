using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Plant : MonoBehaviour
{
    private Rigidbody RB;
    private XRGrabInteractable XRGrab;
    [SerializeField] private bool isGrowing = false;
    [SerializeField] private bool isInPot = false;
    private Collider col;
    [SerializeField] private int water = 0;
    public bool isBeingHeld = false;

    public int[] foodAmountsPerStage = { 2, 4, 8, 16};
    public float[] satiationAmountsPerStage = { 2, 4, 8, 16};
    public int currentStage = 0;
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
        if (isGrowing && transform.parent != null)
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
            if (p != null && !p.GetPlant() && !isBeingHeld)
            {
                p.SetPlant(this);

                isGrowing = true;
                isInPot = true;
                transform.parent = p.seedGrowPoint;
                transform.position = transform.parent.position;

                RB.isKinematic = false;
                RB.velocity = Vector3.zero;
                RB.useGravity = false;
                //col.enabled = false;

                XRGrab.enabled = false;
            }
        }

        if (other.tag.Equals("Chomper") && !isInPot)
        {
            FeedToChomper();
        }
    }

    public void Water(int amount)
    {
        Debug.Log("Adding water, " + name + " now has " + water + " water");
        water += amount;
    }

    public void FeedToChomper()
    {
        ChomperPlant chomp = GameObject.FindAnyObjectByType<ChomperPlant>();
        if (chomp != null) chomp.Feed(this);
    }

    public void SetIsBeingHeld(bool b)
    {
        isBeingHeld = b;
    }
}
