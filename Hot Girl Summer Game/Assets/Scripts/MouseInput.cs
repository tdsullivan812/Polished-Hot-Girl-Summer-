using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public float scrollThreshold; // how close to  the edge of screen the mouse should be before camera scrolls
    public Vector3 cameraXMovementIncrement;
    public Vector3 cameraZMovementIncrement;
    public Camera mainCamera;
    private Vector3 screenMidpoint;
    private int currentLayerMask;
    private float currentForegroundAlpha;
    private float foregroundAlphaIncrement;
    private GameObject[] foregroundObjects;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        screenMidpoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        currentForegroundAlpha = 1.0f;
        foregroundAlphaIncrement = 0.01f;
        foregroundObjects = GameObject.FindGameObjectsWithTag("Env_Foreground");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x <= scrollThreshold && transform.position.x >= -35)
        {
            transform.position -= cameraXMovementIncrement; //scroll left

        }
        else if (Input.mousePosition.x >= Screen.width - scrollThreshold)
        {
            transform.position += cameraXMovementIncrement; //scroll right
        }

        if (Input.mousePosition.y <= scrollThreshold && currentForegroundAlpha < 1)         //&& transform.position.z >= -47)
        {
            //transform.position -= cameraZMovementIncrement; //scroll out
            currentForegroundAlpha += foregroundAlphaIncrement;
            Color newForegroundAlpha = new Color(1, 1, 1, currentForegroundAlpha);
            foreach (GameObject fadingObject in foregroundObjects)
            {
                fadingObject.GetComponentInChildren<SpriteRenderer>().color = newForegroundAlpha;
            }

        }
        else if (Input.mousePosition.y >= Screen.height - scrollThreshold && currentForegroundAlpha > 0)
        {
            //transform.position += cameraZMovementIncrement; // scroll in
            currentForegroundAlpha -= foregroundAlphaIncrement;
            Color newForegroundAlpha = new Color(1, 1, 1, currentForegroundAlpha);
            foreach (GameObject fadingObject in foregroundObjects)
            {
                fadingObject.GetComponentInChildren<SpriteRenderer>().color = newForegroundAlpha;
            }
        }

        //DisableForeground();
    }

    /*
    void DisableForeground()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(screenMidpoint), out hit, Mathf.Infinity, 1 << 9))
        {
            hit.collider.gameObject.SetActive(false);
        }
        else
        {

        }
        
        //GameObject.findob'
       
    }
    */
    void FadeOutAnimation()
    {

    }
}
