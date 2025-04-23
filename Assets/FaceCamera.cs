using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindAnyObjectByType<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform, Vector3.up);
    }
}
