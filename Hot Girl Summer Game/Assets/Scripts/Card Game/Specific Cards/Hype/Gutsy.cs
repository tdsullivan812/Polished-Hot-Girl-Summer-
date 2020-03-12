using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gutsy : Card
{

    public Card[] hypeCards = new Card[2];
    public Gutsy()
    {
        /*
        displayedInfo.cardName = "Gutsy";
        displayedInfo.type = (int)Vibes.Hype;
        displayedInfo.value = 1;
        displayedInfo.isPlayable = true;
        displayedInfo.text = "Immediately add +2 Hype cards to your deck. At the start of your turn, remove those two hype cards from your deck.";
        displayedInfo.art = Resources.Load<Sprite>("");
        */
        InitializeCardGameObject();
    }

    public override void Effect()
    {
        hypeCards[0] = new Hype(); //add two hype cards
        hypeCards[1] = new Hype();
        Encounter.playerDiscard.AddToDiscard(hypeCards[0]);
        Encounter.playerDiscard.AddToDiscard(hypeCards[1]);

        Encounter.BeginningOfTurn.whatHappensAtBeginningOfTurn = RemoveTwoHype;

        //set to destroy cards after opponent's turn

    }

    public void RemoveTwoHype()
    {
        for (int i = 0; i < 2; i++)
        {
            if (Encounter.playerDiscard.cardsInDiscard.Contains(hypeCards[i]))
            {
                Encounter.playerDiscard.cardsInDiscard.Remove(hypeCards[i]);
                continue;
            }
            else if (Encounter.playerDeck.cardsInDeck.Contains(hypeCards[i]))
            {
                Encounter.playerDeck.cardsInDeck.Remove(hypeCards[i]);
                continue;
            }
            else if (Encounter.playerHand.cardsInHand.Contains(hypeCards[i]))
            {
                Encounter.playerHand.cardsInHand.Remove(hypeCards[i]);
                continue;
            }
            else break;
        }
        Encounter.BeginningOfTurn.whatHappensAtBeginningOfTurn = null;
    }
}
