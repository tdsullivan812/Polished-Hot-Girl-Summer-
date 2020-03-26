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
        foreach (Card cardInDiscard in cardsInDiscard)
        {
            cardInDiscard.cardGameObject.SetActive(false);
            cardInDiscard.cardGameObject.GetComponent<CardGUIEvents>().enabled = true;
            cardInDiscard.cardGameObject.transform.SetParent(null);
            cardInDiscard.cardGameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
        cardsInDiscard.Remove(cardToRemove);
        return cardToRemove;
    }


    public Card AddToDiscard(Card cardToAdd)
    {
        cardsInDiscard.Add(cardToAdd);

        if (cardToAdd.cardGameObject.activeInHierarchy)
        {
            cardToAdd.cardGameObject.GetComponent<CardGUIEvents>().StartCoroutine("SendToDiscard");
        }
        else
        {

            cardToAdd.cardGameObject.SetActive(true);
            cardToAdd.cardGameObject.transform.SetPositionAndRotation(Encounter.discardPileTransform.position, Quaternion.identity);
            
            
        }
        cardToAdd.cardGameObject.GetComponent<CardGUIEvents>().enabled = false;
        return cardToAdd;
    }

}
