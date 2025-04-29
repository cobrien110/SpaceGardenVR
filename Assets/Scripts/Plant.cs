using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Plant : MonoBehaviour
{
    private Rigidbody RB;
    private XRGrabInteractable XRGrab;
    [SerializeField] private GameObject tagObj;
    [SerializeField] private bool isGrowing = false;
    [SerializeField] private bool isInPot = false;
    private Collider col;
    [SerializeField] private int water = 0;
    public bool isBeingHeld = false;

    public int[] foodAmountsPerStage = { 2, 4, 8 };
    public float[] satiationAmountsPerStage = { 2, 4, 8 };
    public float[] growTimePerStage = { 0, 5, 10 };
    public float[] waterNeededPerStage = { 10, 20, 30 };
    public int[] valuePerStage = { 5, 6, 8, 10 };
    public Vector3[] colSizePerStage = { };
    private BoxCollider BC;
    public GameObject[] stageModels;
    public GameObject waterIndicator;
    public int currentStage = 0;
    public Slider slider;
    private Pot p;
    private PlantData PD;

    public AudioSource AS;
    public AudioClip[] clips;

    private SellableObject SO;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        XRGrab = GetComponent<XRGrabInteractable>();
        SO = GetComponent<SellableObject>();
        SO.SetValue(valuePerStage[0]);
        BC = GetComponentInChildren<BoxCollider>();
        //SO.SetIsSellable(false);
        col = GetComponentInChildren<Collider>();
        waterIndicator.SetActive(false);
        AS = GetComponent<AudioSource>();
        for (int i = 0; i < stageModels.Length; i++)
        {
            if (i > 0)
            {
                stageModels[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrowing && transform.parent != null)
        {
            transform.position = transform.parent.position;
            transform.rotation = transform.parent.rotation;
        }

        if (waterIndicator != null)
        {
            if (currentStage == stageModels.Length) waterIndicator.SetActive(false);
            else if (water >= waterNeededPerStage[currentStage])
            {
                waterIndicator.SetActive(false);
            } else
            {
                waterIndicator.SetActive(true);
                slider.maxValue = waterNeededPerStage[currentStage];
                slider.value = water;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Bury into pot
        if (other.tag.Equals("Dirt"))
        {
            PD = GetComponent<PlantData>();
            p = other.GetComponent<Pot>();
            if (p != null && !p.GetPlant() && !isBeingHeld)
            {
                p.SetPlant(this);
                PlaySound();

                isGrowing = true;
                isInPot = true;
                transform.parent = p.seedGrowPoint;
                transform.position = transform.parent.position;
                SO.SetIsSellable(true);
                tagObj.SetActive(true);

                RB.isKinematic = false;
                RB.velocity = Vector3.zero;
                RB.useGravity = false;
                
                //col.enabled = false;

                XRGrab.enabled = false;

                SetStage(1);
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

    private void PlaySound()
    {
        AS.clip = clips[Random.Range(0, clips.Length)];
        AS.Play();
    }

    public void SetStage(int num)
    {
        if (currentStage >= stageModels.Length) return;
        // setting the correct model
        /*for (int i = 0; i < stageModels.Length; i++)
        {
            if (i != num)
            {
                stageModels[i].SetActive(false);
            } else
            {
                stageModels[i].SetActive(true);
            }
        }*/

        PD.SetStage(num);
        SO.SetIsSellable(true);
        SO.SetValue(valuePerStage[currentStage]);
        BC.size = colSizePerStage[currentStage];

        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSecondsRealtime(growTimePerStage[currentStage]);
        while (water < waterNeededPerStage[currentStage])
        {

            yield return new WaitForSecondsRealtime(0.25f);
            continue;
        }
        water = 0;
        if (currentStage != stageModels.Length - 1) SetStage(++currentStage);
    }

    public void ResetPot()
    {
        if (p != null) p.SetPlant(null);
    }
}
