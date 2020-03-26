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
            cardInDiscard.cardGameObject.transform.SetParent(null);
            cardInDiscard.cardGameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            cardInDiscard.cardGameObject.GetComponent<CardGUIEvents>();
        }
        cardsInDiscard.Remove(cardToRemove);
        return cardToRemove;
    }

    public Card AddToDiscard(Card cardToAdd)
    {
        cardsInDiscard.Add(cardToAdd);

        if (cardToAdd.cardGameObject.activeInHierarchy)
        {
            Transform initialPosition = cardToAdd.cardGameObject.transform;
            var parameter = 0.0f;
            while (parameter < 1)
            {
                cardToAdd.cardGameObject.transform.SetPositionAndRotation(Vector3.Lerp(initialPosition.position, Encounter.discardPileTransform.position, parameter), Quaternion.identity);
                parameter += 0.001f;
            }



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
