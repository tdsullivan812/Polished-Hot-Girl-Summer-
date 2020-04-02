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
            Encounter.playerDiscard.AddToDiscard(cardToPlay);

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
        cardToDiscard.cardGameObject.GetComponent<CardGUIEvents>().SendToDiscard();
        cardToDiscard.cardGameObject.SetActive(false);

        Encounter.playerDiscard.AddToDiscard(cardToDiscard);


        return cardToDiscard;
    }

    public Card AddToHand(Card cardToAdd)
    {
        cardsInHand.Add(cardToAdd);


        Debug.Log(Encounter.cardGUI.gameObject.name);


        cardToAdd.cardGameObject.transform.SetParent(Encounter.cardGUI.transform);
        cardToAdd.cardGameObject.SetActive(true);
        Debug.Log("Activated object");
        return cardToAdd;
    }
}
