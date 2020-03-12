using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCardEncounter : MonoBehaviour
{
   
    // Update is called once per frame
    public LoadCardEncounter()
    {
        SceneManager.LoadScene("CardEncounter");
    }
}
