using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperPlant : MonoBehaviour
{
    public int hungerLevel = 10;
    public int maxHunger = 20;
    public float satiation = 5;
    public float maxSatiation = 10;
    public float defaultSatiation = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        satiation -= Time.deltaTime;
        if (satiation <= 0)
        {
            satiation = defaultSatiation;
            hungerLevel--;
            Debug.Log("Hunger level decreased to: " + hungerLevel);
        }
    }

    public void Feed(Plant p)
    {
        hungerLevel += p.foodAmountsPerStage[p.currentStage];
        satiation += p.satiationAmountsPerStage[p.currentStage];
        if (satiation > maxSatiation) satiation = maxSatiation;
        Destroy(p.gameObject);

        Debug.Log("Chomper Fed! He has " + hungerLevel + "hunger and is satiated for " + (int) satiation + "seconds.");
    }

}
