using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public GameObject itemToBuy;
    public int cost;
    public ShopManager SM;
    public float force = 4f;
    public int waterUpgrade = 0;
    public TextMeshProUGUI text;
    public string message = "$$";
    private float delay = 1f;
    private float timer = 0f;
    private MeshRenderer MR;
    public Material[] mats;
    private AudioSource AS;
    public AudioClip[] sounds;
    private bool isInteractible = false;
    bool wasBought = false;
    // Start is called before the first frame update
    void Start()
    {
        if (SM == null) SM = GameObject.FindAnyObjectByType<ShopManager>();
        if (text != null) text.text = message + cost;
        MR = GetComponent<MeshRenderer>();
        timer = delay;
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //SM.SpawnAndFire(itemToBuy, force);
        }
        if (timer < delay) timer += Time.deltaTime;

        if (wasBought)
        {
            MR.material = mats[2];
            isInteractible = false;
            return;
        }

        if (timer >= delay)
        {
            if (MR.material != mats[0]) MR.material = mats[0];
            isInteractible = true;
        } else if (timer > 0 && timer < 0.15f)
        {
            if (MR.material != mats[1]) MR.material = mats[1];
            isInteractible = false;
        } else
        {
            if (MR.material != mats[2]) MR.material = mats[2];
            isInteractible = false;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!isInteractible)
        {
            // play fail sound
            if (AS != null && sounds[0] != null)
            {
                AS.clip = sounds[1];
                AS.pitch = Random.Range(0.9f, 1.1f);
                AS.volume = 1f;
                AS.Play();
            }
        } else
        {
            // play success sound
            AS.clip = sounds[0];
            AS.pitch = Random.Range(0.9f, 1.1f);
            AS.volume = 0.5f;
            AS.Play();

            // buy upgrade / item
            if (waterUpgrade == 0)
            {
                if (itemToBuy == null) return;
                SM.SpawnAndFire(itemToBuy, force, cost);
            }
            else if (waterUpgrade == 1) //fireRate
            {
                SM.Upgrade(1, cost);
                wasBought = true;
            }
            else if (waterUpgrade == 2) //amount
            {
                SM.Upgrade(2, cost);
            }
            timer = 0f;
        }
        
    }
}
