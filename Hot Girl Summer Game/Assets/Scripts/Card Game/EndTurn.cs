using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{

    public void OnClick()
    {
        if (Encounter.cardGameFSM.CurrentState.GetType() == typeof(Encounter.PlayerTurn))
        {
            Services.encounter.ChangeTurn();
            gameObject.SetActive(false);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
