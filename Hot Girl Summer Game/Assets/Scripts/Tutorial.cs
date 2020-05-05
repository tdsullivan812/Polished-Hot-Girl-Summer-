using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private static bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        if (!firstTime)
        {
            gameObject.SetActive(false);
        }
        else
        {
            firstTime = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
