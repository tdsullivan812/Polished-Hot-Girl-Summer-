using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

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
    public string[] arrayOfBuyEncounters;
    public string[] arrayOfCardEncounters;
    public Fungus.Flowchart flowchart;
    private List<string> listOfBuyEncounters;
    private List<string> listOfCardEncounters;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        screenMidpoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        currentForegroundAlpha = 1.0f;
        foregroundAlphaIncrement = 0.01f;
        foregroundObjects = GameObject.FindGameObjectsWithTag("Env_Foreground");
        listOfBuyEncounters = new List<string>(arrayOfBuyEncounters);
        listOfCardEncounters = new List<string>(arrayOfCardEncounters);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }

        if (Input.mousePosition.x <= scrollThreshold && transform.position.x >= -35)
        {
            transform.position -= cameraXMovementIncrement; //scroll left

        }
        else if (Input.mousePosition.x >= Screen.width - scrollThreshold && transform.position.x <= 17)
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

        if (currentForegroundAlpha <= 0.05f)
        {
            foreach (GameObject foreground in foregroundObjects) foreground.SetActive(false);
        }
        else if (!foregroundObjects[0].active)
        {
            foreach (GameObject foreground in foregroundObjects) foreground.SetActive(true);
        }

        ClickOnNPC();
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

    void ClickOnNPC()
    { /*
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Physics.Raycast(Input.mousePosition, Vector3.forward, out hit, Mathf.Infinity);
            string NPCTag = hit.collider.gameObject.tag;
            if (listOfBuyEncounters.Contains(NPCTag))
            {
                flowchart.ExecuteBlock(NPCTag + " Buy Encounter");
            }
            else if (listOfCardEncounters.Contains(NPCTag))
            {
                flowchart.ExecuteBlock(NPCTag + " Problem");
            }
        } */
    }
    
}
