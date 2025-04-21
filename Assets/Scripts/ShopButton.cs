using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject itemToBuy;
    public int cost;
    public ShopManager SM;
    public float force = 4f;
    public int waterUpgrade = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (SM == null) SM = GameObject.FindAnyObjectByType<ShopManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //SM.SpawnAndFire(itemToBuy, force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (waterUpgrade == 0)
        {
            if (itemToBuy == null) return;
            SM.SpawnAndFire(itemToBuy, force, cost);
        } else if (waterUpgrade == 1)
        {
            SM.Upgrade(1, cost);
        } else if (waterUpgrade == 2)
        {
            SM.Upgrade(2, cost);
        }
    }
}
