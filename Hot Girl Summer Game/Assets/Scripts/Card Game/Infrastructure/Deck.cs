using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : ICardGameElement
{
    
    public List<Card> cardsInDeck;
    public DeckList deckList;

    public Deck()
    {
        deckList = GameController.partyDeck;
        cardsInDeck = new List<Card>();
        cardsInDeck.AddRange(deckList.allCards);
        Shuffle();
    }
    
    public Card Draw()
    {
        Debug.Assert(cardsInDeck.Count > 0, "Assertion Failed: Deck is empty");
        var totalCardsInDiscardPile = Encounter.playerDiscard.cardsInDiscard.Count;

        if (cardsInDeck.Count == 0)
        {
            foreach (Card currentlyInDiscard in Encounter.playerDiscard.cardsInDiscard)
            {
                
                Encounter.playerDeck.Add(currentlyInDiscard);
            }

            foreach (Card justAddedToDeck in Encounter.playerDeck.cardsInDeck)
            {
                Encounter.playerDiscard.Remove(justAddedToDeck);
            }
            Shuffle(); 
        }
        var cardToDraw = cardsInDeck[0];
        Remove(cardToDraw);
        //Encounter.playerHand.AddToHand(cardToDraw);
        Debug.Log("drew card");
        return Encounter.playerHand.AddToHand(cardToDraw);
    }
    

    public Card Add(Card cardToAdd, int targetPosition = -1)
    {
        if (targetPosition == -1)
        {
            cardsInDeck.Add(cardToAdd);
            return cardToAdd;
        }
        cardsInDeck.Insert(targetPosition, cardToAdd);

        if (cardToAdd.cardGameObject == null)
        {
            cardToAdd.AssignGameObject();
            cardToAdd.cardGameObject.transform.SetParent(GameObject.Find("Canvas").transform);


        }
        cardToAdd.cardGameObject.GetComponent<CardGUIEvents>().StartCoroutine("SendToDeck");
        //cardToAdd.cardGameObject.GetComponent<CardGUIEvents>().enabled = false;
        return cardToAdd;
    }

    public Card Remove(Card cardToRemove)
    {
        if (cardToRemove.cardGameObject == null)
        {
            cardToRemove.AssignGameObject();
            cardToRemove.cardGameObject.transform.SetParent(GameObject.Find("Canvas").transform);


        }

        
        cardToRemove.cardGameObject.transform.SetPositionAndRotation(Encounter.deckZone.transform.position, Quaternion.identity);
        cardToRemove.cardGameObject.SetActive(true);
        cardsInDeck.Remove(cardToRemove);
        return cardToRemove;
    }

    public void Shuffle()
    {
        List<Card> newList = new List<Card>();
        newList.AddRange(cardsInDeck);
        for (int i = 0; i <cardsInDeck.Count; i++)
        {
            int temporaryInt = (int)Mathf.Floor(Random.Range(0, newList.Count));
            cardsInDeck[i] = newList[temporaryInt];
            newList.RemoveAt(temporaryInt);
        }

    }


}
