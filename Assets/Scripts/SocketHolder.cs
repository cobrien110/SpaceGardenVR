using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketHolder : MonoBehaviour
{
    //public Transform XRRig;
    public Transform cam; 
    public float yOffset;
    public float rotationLerpSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        transform.position = new Vector3(cam.position.x, yOffset, cam.position.z);

        // Get the head's forward direction on the horizontal plane
        Vector3 flatForward = cam.forward;
        flatForward.y = 0;
        flatForward.Normalize();

        // Smoothly rotate the socket to match the head's Y rotation (only yaw)
        if (flatForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatForward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationLerpSpeed);
        }
    }
}
