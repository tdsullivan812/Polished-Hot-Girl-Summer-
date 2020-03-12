using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchRoom : MonoBehaviour
{
    
    public bool isFrontArea;
    public bool isBasement;
    public GameObject thisForeground;
    public string thisRoomsName;
    public GameObject textObject;

    private TextMeshProUGUI currentRoom;

    private GameObject[] foregrounds;


    void OnTriggerEnter(Collider collision)
    {

        if (isFrontArea) { 
            thisForeground.SetActive(false);//disable foreground
        }

        else if (!isFrontArea)
        {
            foregrounds = GameObject.FindGameObjectsWithTag("FF_Element"); //disable all of front area

            foreach (GameObject t in foregrounds)
            {
                t.SetActive(false);
            }
        }

        currentRoom = textObject.GetComponent<TextMeshProUGUI>(); //set room name
        currentRoom.SetText(thisRoomsName);


    }

    void OnTriggerExit(Collider collision) //note to self: issues when colliding with walls. prolly will work better w/ colliders
    {
        if (isFrontArea)
        {
            thisForeground.SetActive(true); //enable foreground
        }

        else if (!isFrontArea)
        {
            foregrounds = GameObject.FindGameObjectsWithTag("FF_Element"); //enable all of front area

            foreach (GameObject t in foregrounds)
            {
                t.SetActive(true);
            }

        }
    }
}
