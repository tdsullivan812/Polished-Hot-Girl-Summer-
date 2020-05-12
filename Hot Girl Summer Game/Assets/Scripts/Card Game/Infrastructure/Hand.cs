using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    public List<Card> cardsInHand;
    //public List<RectTransform> handTransforms;

    public Hand()
    {
        cardsInHand = new List<Card>();

    }

    public Card PlayFromHand(Card cardToPlay)
    {
        if(Encounter.playerActions > 0)
        {
            Encounter.playerActions--;
            cardsInHand.Remove(cardToPlay);
            Encounter.playerDiscard.Add(cardToPlay);

            //handTransforms.RemoveAt(0);
            //Services.encounter.UpdateHandSize();
            cardToPlay.cardGameObject.transform.SetParent(Encounter.discardPileTransform);
            //cardToPlay.cardGameObject.SetActive(false);
            //Services.encounter.UpdateCardGameObjects();
            Services.encounter.Play(cardToPlay);

            
        }
        return cardToPlay;
    }

    public Card Discard(Card cardToDiscard)
    {
        cardsInHand.Remove(cardToDiscard);

        cardToDiscard.cardGameObject.transform.SetParent(null);
        Encounter.objectPools[cardToDiscard.displayedInfo.cardName].Push(cardToDiscard.cardGameObject);
        cardToDiscard.cardGameObject.GetComponent<CardGUIEvents>().SendToDiscard();
        //cardToDiscard.cardGameObject.SetActive(false);
        //InputManager.activeCardGameObjects.Remove(cardToDiscard.cardGameObject);

        Encounter.playerDiscard.Add(cardToDiscard);


        return cardToDiscard;
    }

    public Card AddToHand(Card cardToAdd)
    {
        cardsInHand.Add(cardToAdd);


        Debug.Log(Encounter.cardGUI.gameObject.name);
        cardToAdd.AssignGameObject();

        /*
        string currentCardName = cardToAdd.displayedInfo.cardName;
        GameObject cardObjectToPutInHand;
        if (Encounter.objectPools.ContainsKey(currentCardName) == false) //check if there is an existing pool for this card; if not, make one
        {
            cardToAdd.InitializeCardGameObject();
            cardObjectToPutInHand = cardToAdd.cardGameObject;
        }
        else if (Encounter.objectPools[currentCardName].Count == 0) //if there is a pool, check if there are any of this card in it; if not, instantiate a new object
        {
           cardObjectToPutInHand = Object.Instantiate(Encounter.objectPools[cardToAdd.displayedInfo.cardName].cardGameObject);
        }
        else // if there is a card in the pool, just pop it
        {
            cardObjectToPutInHand = Encounter.objectPools[currentCardName].Pop();
        }
        cardToAdd.cardGameObject = cardObjectToPutInHand; */

        cardToAdd.cardGameObject.transform.SetParent(Encounter.cardGUI.transform);
        cardToAdd.cardGameObject.SetActive(true);
        //InputManager.activeCardGameObjects.Add(cardToAdd.cardGameObject);
        Debug.Log("Activated object");
        return cardToAdd;
    }
}
