using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discard : ICardGameElement
{

   
    public List<Card> cardsInDiscard;

    public Discard()
    {
        cardsInDiscard = new List<Card>();
    }

    public Card Remove(Card cardToRemove)
    {
        /*
        foreach (Card cardInDiscard in cardsInDiscard)
        {
            cardInDiscard.cardGameObject.SetActive(false);
            cardInDiscard.cardGameObject.GetComponent<CardGUIEvents>().enabled = true;
            cardInDiscard.cardGameObject.transform.SetParent(null);
            cardInDiscard.cardGameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }*/
        if (cardToRemove.cardGameObject == null)
        {
            cardToRemove.AssignGameObject();
            cardToRemove.cardGameObject.transform.SetParent(GameObject.Find("Canvas").transform);


        }
        cardToRemove.cardGameObject.transform.SetPositionAndRotation(Encounter.deckZone.transform.position, Quaternion.identity);
        cardToRemove.cardGameObject.SetActive(true);
        cardsInDiscard.Remove(cardToRemove);
        return cardToRemove;
    }


    public Card Add(Card cardToAdd, int target = -1)
    {
        if (target == -1)
        {
            cardsInDiscard.Add(cardToAdd);
        }
        else
        {
            cardsInDiscard.Insert(target, cardToAdd);
        }


        if (cardToAdd.cardGameObject == null)
        {
            cardToAdd.AssignGameObject();
            cardToAdd.cardGameObject.transform.SetParent(GameObject.Find("Canvas").transform);

            
        }
        cardToAdd.cardGameObject.SetActive(true);
        cardToAdd.cardGameObject.GetComponent<CardGUIEvents>().StartCoroutine("SendToDiscard");
        //cardToAdd.cardGameObject.transform.SetPositionAndRotation(Encounter.discardPileTransform.position, Quaternion.identity);
        //cardToAdd.cardGameObject.GetComponent<CardGUIEvents>().enabled = false;
        return cardToAdd;
    }

}
