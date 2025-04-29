using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChomperPlant : MonoBehaviour
{
    public int hungerLevel = 10;
    public int maxHunger = 20;
    public float satiation = 5;
    public float maxSatiation = 10;
    public float defaultSatiation = 2.5f;
    public GameObject mouthPoint;
    public float rangeChomperEat = 1.5f;
    public Slider slider;
    public Image fill;
    public Color[] fillColors;

    private AudioSource AS;
    public AudioClip[] clips;
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        satiation -= Time.deltaTime;
        if (satiation <= 0)
        {
            satiation = defaultSatiation;
            if (hungerLevel > 0) hungerLevel--;
            Debug.Log("Hunger level decreased to: " + hungerLevel);
        }

        SetFillColor();
        slider.value = (float) hungerLevel / (float) maxHunger;
    }

    public void Feed(Plant p)
    {
        hungerLevel += p.foodAmountsPerStage[p.currentStage];
        satiation += p.satiationAmountsPerStage[p.currentStage];
        if (satiation > maxSatiation) satiation = maxSatiation;
        Destroy(p.gameObject);
        PlaySound();

        Debug.Log("Chomper Fed! He has " + hungerLevel + "hunger and is satiated for " + (int) satiation + "seconds.");
    }

    private void SetFillColor()
    {
        if (satiation <= 1 && (int) Time.time % 2 == 0)
        {
            fill.color = fillColors[1];
        } else if ((float)hungerLevel / (float)maxHunger < .2f)
        {
            fill.color = fillColors[3];
        }
        else if ((float) hungerLevel / (float) maxHunger < .5f)
        {
            fill.color = fillColors[2];
        } else
        {
            fill.color = fillColors[0];
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(mouthPoint.transform.position, rangeChomperEat);
    }

    private void PlaySound()
    {
        AS.clip = clips[Random.Range(0, clips.Length)];
        AS.Play();
    }

}
