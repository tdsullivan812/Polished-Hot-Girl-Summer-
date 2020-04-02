using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardFromMenu : MonoBehaviour
{
    public GameObject[] cardsCurrentlyShown;
    public int numberOfCardsShown;
    // Start is called before the first frame update
    void Start()
    {
        cardsCurrentlyShown = new GameObject[5];
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateCardsShown()
    {
        for (int i = 0; i < numberOfCardsShown; i++)
        {
            cardsCurrentlyShown[i].transform.SetParent(gameObject.transform);
        }
    }
}
