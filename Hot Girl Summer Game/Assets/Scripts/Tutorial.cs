using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EndTutorial()
    {
        //gameObject.SetActive(false);
        Encounter.cardGameFSM.TransitionTo<Encounter.BeginningOfTurn>();
        InputManager.inputFSM.TransitionTo<CardEncounterPlayerTurn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
