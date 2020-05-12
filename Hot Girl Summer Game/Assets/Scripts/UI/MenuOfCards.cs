using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuOfCards : MonoBehaviour
{
    public enum CardElement : byte
    {
        deck, 
        discard 
    }

    public CardElement connectedElementInput; //indicates whether clicking on this object shows the Deck or Discard; set in Editor

    public ICardGameElement connectedElementObject; //The actual Deck or Discard object that is referenced; determined at runtime

    public  GameObject menuObject; // The GameObject that serves as the menu

    public  GameObject grid; // The GameObject with a Grid Layout component. It will be the parent of card gameobjects

    public  Scrollbar scrollbar; //The scrollbar component used to scroll through cards when there are too many to fit on the menu

    public static List<Card> cardsToDisplay; //The list of cards in the deck/discard pile

    public static Comparison<Card> comparison; //A method used to sort cards alphabetically 

    public static GameObject[] gameObjectsInGrid; //An array of gameobjects CURRENTLY displayed on the menu

    public static int arrayLength; //The size of the gameObjectsInGrid array

    public static int index; // an index of which card appears in the upper left corner of the menu

    private TextMeshProUGUI _cardCounter;

    // Start is called before the first frame update
    void Start()
    {
        /*
        switch (connectedElementInput)
        {
            case CardElement.deck:
                Debug.Log(Encounter.playerDeck);
                connectedElementObject = (ICardGameElement)Encounter.playerDeck;
                break;
            case CardElement.discard:
                connectedElementObject = (ICardGameElement)Encounter.playerDiscard;
                break;
            default:
                break;
        }

        //menuObject = GameObject.Find("MenuCanvas");
        //grid = GameObject.Find("GridLayout");
        //scrollbar = GameObject.Find("ScrollBarTrack").GetComponent<Scrollbar>();
        comparison = AlphabeticalCompare; */
    }


    public void Initialize()
    {
        switch (connectedElementInput)
        {
            case CardElement.deck:
                Debug.Log(Encounter.playerDeck);
                connectedElementObject = (ICardGameElement)Encounter.playerDeck;
                break;
            case CardElement.discard:
                connectedElementObject = (ICardGameElement)Encounter.playerDiscard;
                break;
            default:
                break;
        }

        comparison = AlphabeticalCompare;
        _cardCounter = gameObject.GetComponentInChildren<TextMeshProUGUI>();

    }

    public void TriggerMenu()
    {
        menuObject.SetActive(true);
        switch (connectedElementInput) //If player clicked on Deck, loads cards in Deck; if they clicked on Discard, loads cards in Discard
        {
            case CardElement.deck:
                menuObject.GetComponentInChildren<TextMeshProUGUI>().text = "Deck";
                cardsToDisplay = new List<Card>(Encounter.playerDeck.cardsInDeck);
                cardsToDisplay.Sort(comparison);
                break;
            case CardElement.discard:
                menuObject.GetComponentInChildren<TextMeshProUGUI>().text = "Discard";
                cardsToDisplay = Encounter.playerDiscard.cardsInDiscard;
                break;
            default:
                break;
        }

        InputManager.inputFSM.TransitionTo<CardEncounterMenu>(); //Transition to the input style for menus

        arrayLength = (int)Mathf.Min(cardsToDisplay.Count, 9);
        gameObjectsInGrid = new GameObject[arrayLength];
        for (int i = 0; i < arrayLength; i++)
        {
            cardsToDisplay[i].AssignGameObject();
            /*
            string currentCardName = cardsToDisplay[i].displayedInfo.cardName;
            GameObject cardObjectToPutInMenu;

            
            if (Encounter.objectPools.ContainsKey(currentCardName) == false) //check if there is an existing pool for this card; if not, make one
            {
                cardsToDisplay[i].InitializeCardGameObject();
                cardObjectToPutInMenu = cardsToDisplay[i].cardGameObject;
            }
            else if (Encounter.objectPools[currentCardName].Count == 0) //if there is a pool, check if there are any of this card in it; if not, instantiate a new object
            {
                cardObjectToPutInMenu = UnityEngine.Object.Instantiate(Encounter.objectPools[cardsToDisplay[i].displayedInfo.cardName].cardGameObject);
            }
            else // if there is a card in the pool, just pop it
            {
                cardObjectToPutInMenu = Encounter.objectPools[currentCardName].Pop();
            }
            cardsToDisplay[i].cardGameObject = cardObjectToPutInMenu;*/

            cardsToDisplay[i].cardGameObject.transform.SetParent(grid.transform); //Set the parent to the Grid Layout object
            cardsToDisplay[i].cardGameObject.SetActive(true);

            gameObjectsInGrid[i] = cardsToDisplay[i].cardGameObject;
            //gameObjectsInGrid[i].transform.SetParent(grid.transform);
        }
        Debug.Log(gameObjectsInGrid.Length);
    }

    public void DisableMenu()
    {
        for (int i = 0; i < gameObjectsInGrid.Length; i++)
        {
            Encounter.objectPools[gameObjectsInGrid[i].GetComponent<CardIdentifier>().whichCardIsThis.displayedInfo.cardName].Push(gameObjectsInGrid[i]);
            gameObjectsInGrid[i].transform.SetParent(null);
            
        }
        menuObject.SetActive(false);
        InputManager.inputFSM.TransitionTo<CardEncounterPlayerTurn>();
    }


    public int AlphabeticalCompare(Card a, Card b) //a way of comparing the alphabetical order of two cards. Used to sort the Deck alphabetically
    {
        return string.Compare(a.displayedInfo.cardName, b.displayedInfo.cardName, true);
    }

    public void ScrollThroughMenu()
    {
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
            Encounter.objectPools[gameObjectsInGrid[i].GetComponent<CardIdentifier>().whichCardIsThis.displayedInfo.cardName].Push(gameObjectsInGrid[i]);
            int adjustedIndex = (index + i) % 9;

            cardsToDisplay[adjustedIndex].AssignGameObject();
            //This sequence manages checking and retrieving the card gameObjects from the object pools

            /*
            string currentCardName = cardsToDisplay[adjustedIndex].displayedInfo.cardName;
            GameObject cardObjectToPutInMenu;
            if (Encounter.objectPools.ContainsKey(currentCardName) == false) //check if there is an existing pool for this card; if not, make one
            {
                cardsToDisplay[adjustedIndex].InitializeCardGameObject();
                cardObjectToPutInMenu = cardsToDisplay[adjustedIndex].cardGameObject;
            }
            else if (Encounter.objectPools[currentCardName].Count == 0) //if there is a pool, check if there are any of this card in it; if not, instantiate a new object
            {
                cardObjectToPutInMenu = UnityEngine.Object.Instantiate(Encounter.objectPools[cardsToDisplay[adjustedIndex].displayedInfo.cardName].cardGameObject);
            }
            else // if there is a card in the pool, just pop it
            {
                cardObjectToPutInMenu = Encounter.objectPools[currentCardName].Pop();
            }
            cardsToDisplay[adjustedIndex].cardGameObject = cardObjectToPutInMenu; */
            cardsToDisplay[adjustedIndex].cardGameObject.transform.SetParent(grid.transform); //Set the parent to the Grid Layout object
            cardsToDisplay[adjustedIndex].cardGameObject.SetActive(true);

            gameObjectsInGrid[i] = cardsToDisplay[adjustedIndex].cardGameObject; //finally, records the new object as being in the grid
        }
    }

    void Update()
    {
        if (_cardCounter != null)
        {
            switch (connectedElementInput)
            {
                case CardElement.deck:
                    _cardCounter.text = Encounter.playerDeck.cardsInDeck.Count.ToString();
                    break;
                case CardElement.discard:
                    _cardCounter.text = Encounter.playerDiscard.cardsInDiscard.Count.ToString();
                    break;
                default:
                    break;
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MenuOfCards)), CanEditMultipleObjects]
    public class MenuScriptEditor : Editor
    {
        SerializedProperty m_menuObject;
        SerializedProperty m_grid;
        SerializedProperty m_scrollbar;

        private void OnEnable()
        {
            m_menuObject = serializedObject.FindProperty("menuObject");
            m_grid = serializedObject.FindProperty("grid");
            m_scrollbar = serializedObject.FindProperty("scrollbar");
        }
        public override void OnInspectorGUI()
        {
            
            var myTarget = (MenuOfCards)target;
            myTarget.connectedElementInput = (CardElement)EditorGUILayout.EnumPopup("Deck or Discard?", myTarget.connectedElementInput);
            //myTarget.menuObject = (GameObject)EditorGUILayout.ObjectField("Menu", (UnityEngine.Object)myTarget.menuObject, typeof(GameObject), true);
            //myTarget.grid = (GameObject)EditorGUILayout.ObjectField("Grid", (UnityEngine.Object)myTarget.grid, typeof(GameObject), true);
            //myTarget.scrollbar = (Scrollbar)EditorGUILayout.ObjectField("Scrollbar", (Scrollbar)myTarget.scrollbar, typeof(Scrollbar), true);

            /*
            myTarget.connectedElementInput = (CardElement)EditorGUILayout.EnumPopup("Deck or Discard?", myTarget.connectedElementInput);
            serializedObject.FindProperty("menuObject").exposedReferenceValue = (GameObject)EditorGUILayout.ObjectField("Menu", (UnityEngine.Object)MenuOfCards.menuObject, typeof(GameObject), true);
            serializedObject.FindProperty("grid").exposedReferenceValue = (GameObject)EditorGUILayout.ObjectField("Grid", (UnityEngine.Object)MenuOfCards.grid, typeof(GameObject), true);
            serializedObject.FindProperty("scrollbar").exposedReferenceValue = (Scrollbar)EditorGUILayout.ObjectField("Scrollbar", (Scrollbar)MenuOfCards.scrollbar, typeof(Scrollbar), true);*/

            EditorGUILayout.PropertyField(m_menuObject, new GUIContent("Menu"));
            EditorGUILayout.PropertyField(m_grid, new GUIContent("Grid"));
            EditorGUILayout.PropertyField(m_scrollbar, new GUIContent("Scrollbar"));

            serializedObject.ApplyModifiedProperties();

        }
    }
#endif
}
