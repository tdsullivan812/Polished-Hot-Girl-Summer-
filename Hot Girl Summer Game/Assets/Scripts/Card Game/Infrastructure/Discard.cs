using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discard
{

   
    public List<Card> cardsInDiscard;

    public Discard()
    {
        cardsInDiscard = new List<Card>();
    }

    public Card RemoveFromDiscard(Card cardToRemove)
    {
        cardsInDiscard.Remove(cardToRemove);
        return cardToRemove;
    }

    public Card AddToDiscard(Card cardToAdd)
    {
        cardsInDiscard.Add(cardToAdd);
        return cardToAdd;
    }

}
