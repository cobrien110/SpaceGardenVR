using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float dirRandomRange = 0.15f;
    [SerializeField] private float launchForce = 0.5f;
    private StatTracker ST;
    public float fireRatePerUpgrade = 0.05f;
    public TextMeshProUGUI text;
    private PlantFormation PF;
    // Start is called before the first frame update
    void Start()
    {
        ST = GameObject.FindAnyObjectByType<StatTracker>();
        PF = GameObject.FindAnyObjectByType<PlantFormation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (text != null) text.text = "MONEY: $" + ST.money;
    }

    public void SpawnAndFire(GameObject item)
    {
        Debug.Log("Spawning shop item: " + item.name);
        GameObject drop = Instantiate(item, spawnLocation.position, spawnLocation.rotation);

        if (drop.GetComponent<Plant>())
        {
            PF.ConstructPlant(drop);
        }

        Rigidbody RB = drop.GetComponent<Rigidbody>();
        if (RB == null) return;

        //get random force dir
        float dirX = Random.Range(-dirRandomRange, dirRandomRange);
        float dirY = Random.Range(-dirRandomRange, dirRandomRange);

        // Start with forward direction
        Vector3 baseDir = spawnLocation.forward;

        // Add slight variation in right and up directions
        Vector3 variedDir = baseDir + spawnLocation.right * dirX + spawnLocation.up * dirY;

        // Normalize to maintain consistent force
        Vector3 finalForce = variedDir.normalized * launchForce;

        RB.AddForce(finalForce, ForceMode.Impulse); // optional: use Impulse for instant push
    }

    public void SpawnAndFire(GameObject item, float force, int cost)
    {
        if (cost > ST.money)
        {
            Debug.Log("not enough money for purchase");
            return;
        }
        Debug.Log("Spawning shop item: " + item.name);
        GameObject drop = Instantiate(item, spawnLocation.position, spawnLocation.rotation);

        if (drop.GetComponent<Plant>())
        {
            PF.ConstructPlant(drop);
        }

        Rigidbody RB = drop.GetComponent<Rigidbody>();
        if (RB == null) return;

        //get random force dir
        float dirX = Random.Range(-dirRandomRange, dirRandomRange);
        float dirY = Random.Range(-dirRandomRange, dirRandomRange);

        // Start with forward direction
        Vector3 baseDir = spawnLocation.forward;

        // Add slight variation in right and up directions
        Vector3 variedDir = baseDir + spawnLocation.right * dirX + spawnLocation.up * dirY;

        // Normalize to maintain consistent force
        Vector3 finalForce = variedDir.normalized * force;

        RB.AddForce(finalForce, ForceMode.Impulse); // optional: use Impulse for instant push

        ST.money -= cost;
    }

    public void Upgrade(int i, int cost)
    {
        if (cost > ST.money)
        {
            Debug.Log("not enough money for purchase");
            return;
        }
        if (i == 1)
        {
            ST.waterCanFireRate -= fireRatePerUpgrade;
            if (ST.waterCanFireRate < 0.05f) ST.waterCanFireRate = 0.05f;
        } else if (i == 2)
        {
            ST.waterCanDropAmount += 1;
        }

        ST.money -= cost;
    }
}
