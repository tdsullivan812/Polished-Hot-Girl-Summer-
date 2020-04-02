using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogueTrigger : MonoBehaviour
{
    public Fungus.Flowchart myFlowchart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.tag == "Warren")
        {
            Debug.Log("hit warren");
            myFlowchart.ExecuteBlock("Warren Buy Encounter");
        }

        if (collision.gameObject.tag == "Kelly")
        {
            Debug.Log("hit kelly");
            myFlowchart.ExecuteBlock("Kelly Problem");
        }
    }
}
