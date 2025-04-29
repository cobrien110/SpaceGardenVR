using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour
{
    private AudioSource AS;
    public AudioClip[] clips;
    private int counter = 0;
    public float songTime;
    public bool inputTime = false;
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        counter = Random.Range(0, clips.Length);
        AS.clip = clips[counter];
        AS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!AS.isPlaying)
        {
            counter++;
            if (counter >= clips.Length) counter = 0;
            AS.clip = clips[counter];
            AS.Play();
        }
        if (!inputTime) songTime = AS.time;
        else AS.time = songTime;
    }
}
