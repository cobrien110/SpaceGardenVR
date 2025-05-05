using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsCheck : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerStart;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.Find("Main Camera");
        playerStart = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " hit trig");

        other.transform.position = playerStart;
    }
}
