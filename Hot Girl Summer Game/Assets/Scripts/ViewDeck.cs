using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDeck : MonoBehaviour
{
    List<Card> cardsToDisplay;
    public GameObject menuObject;
    public GameObject grid;
    public UnityEngine.UI.Scrollbar scrollbar;
    private int arrayLength;
    private GameObject[] gameObjectsInGrid;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerMenu()
    {
        menuObject.SetActive(true);
        cardsToDisplay = new List<Card>(GameController.partyDeck.allCards);
        foreach(Card card in cardsToDisplay)
        {
            card.InitializeCardGameObject();
        }
        arrayLength = (int)Mathf.Min(cardsToDisplay.Count, 9);
        gameObjectsInGrid = new GameObject[arrayLength];
        for (int i = 0; i < arrayLength; i++)
        {
            
            cardsToDisplay[i].cardGameObject.transform.SetParent(grid.transform); //Set the parent to the Grid Layout object
            cardsToDisplay[i].cardGameObject.SetActive(true);

            gameObjectsInGrid[i] = cardsToDisplay[i].cardGameObject;
            //gameObjectsInGrid[i].transform.SetParent(grid.transform);
        }


    }

    public void DisableMenu()
    {
        foreach (Card card in cardsToDisplay)
        {
            GameObject.Destroy(card.cardGameObject);

        }
        menuObject.SetActive(false);
    }

    public void ScrollThroughMenu()
    {
        int index;
        int numberOfExtraRows = (cardsToDisplay.Count / 9);
        float stepValue = 1.0f / (numberOfExtraRows + 1);

        if (stepValue == 1.0f) index = 0;
        else
        {
            index = 3 * Mathf.FloorToInt(scrollbar.value / stepValue);
        }

        for (int i = 0; i < gameObjectsInGrid.Length; i++)
        {
            gameObjectsInGrid[i].transform.SetParent(null); // first, unparents whatever is currently in the grid
            int adjustedIndex = (index + i) % 9;


            cardsToDisplay[adjustedIndex].cardGameObject.transform.SetParent(grid.transform); //Set the parent to the Grid Layout object
            cardsToDisplay[adjustedIndex].cardGameObject.SetActive(true);

            gameObjectsInGrid[i] = cardsToDisplay[adjustedIndex].cardGameObject; //finally, records the new object as being in the grid
        }
    }
}
