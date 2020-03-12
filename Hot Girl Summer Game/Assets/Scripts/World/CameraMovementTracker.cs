using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementTracker : MonoBehaviour
{
    public Transform trackThis; //this is the object that this camera will track
    
    // Update is called once per frame
    void Update()
    { 
        transform.position = new Vector3(trackThis.position.x, transform.position.y, transform.position.z); //follow x movement

        //track character depth
        if(trackThis.position.z > 7.5) //if player is in back half, move camera 
        {
            transform.position = new Vector3(trackThis.position.x, transform.position.y, 1f); //follow x movement
        }
        if (trackThis.position.z <= 2.5) //if player is in front half, move camera
        {
            transform.position = new Vector3(trackThis.position.x, transform.position.y, -14f); //follow x movement
        }

    }

}
