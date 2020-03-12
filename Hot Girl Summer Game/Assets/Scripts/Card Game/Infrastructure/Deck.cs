using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
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
                
                Encounter.playerDeck.AddToDeck(currentlyInDiscard);
            }

            foreach (Card justAddedToDeck in Encounter.playerDeck.cardsInDeck)
            {
                Encounter.playerDiscard.RemoveFromDiscard(justAddedToDeck);
            }
            Shuffle(); 
        }
        var cardToDraw = cardsInDeck[0];
        RemoveFromDeck(cardToDraw);
        //Encounter.playerHand.AddToHand(cardToDraw);
        Debug.Log("drew card");
        return Encounter.playerHand.AddToHand(cardToDraw);
    }
    

    public Card AddToDeck(Card cardToAdd, int targetPosition = -1)
    {
        if (targetPosition == -1)
        {
            cardsInDeck.Add(cardToAdd);
            return cardToAdd;
        }
        cardsInDeck.Insert(targetPosition, cardToAdd);
        return cardToAdd;
    }

    public Card RemoveFromDeck(Card cardToRemove)
    {
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
