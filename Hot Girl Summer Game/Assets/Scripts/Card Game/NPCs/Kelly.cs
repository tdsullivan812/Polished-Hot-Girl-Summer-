﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kelly : NPC
{
    private GameObject _selectACardMenu;
    private GameObject menuGrid;
    public int index;
    protected List<Card> cardsInTheMenu;


    public Kelly()
    {
        npcName = "Kelly";
        npcSprite = Services.gameController.arrayOfSprites[(int)NPCSprites.Parse(npcName)];
        victoryCondition = RequiredPoints;
        successMessage = "Kelly Solved";
        failureMessage = "Kelly Unsolved";
        turnsExpired = 0;
        description = "Have 16 or more points of any Vibe to help Kelly feel calm. Every turn, she'll permanently remove a Hype or Bubbly card from your discard!";

        //parentGameObject.SetActive(false);
    }

    public bool RequiredPoints()
    {
        if (GameController.partyDeck.victoryPoints.totalPoints >= 16)
        {
            return true;
        }
        else return false;
    }


    public override void Effect()
    {
        
        Debug.Log("Kelly Effect");
        //Encounter.WaitForInput.whatAmIWaitingFor = SelectCardToRemove;
        //Encounter.cardGameFSM.TransitionTo<Encounter.WaitForInput>();
        //Encounter._selectACardMenu.SetActive(true);
        //Encounter.cardDropZone.SetActive(false);
        cardsInTheMenu = new List<Card>();
        //index = 0;
        foreach (Card partyDeckCard in GameController.partyDeck.allCards)
        {
            if ((partyDeckCard.displayedInfo.type == Card.Vibes.Bubbly) || (partyDeckCard.displayedInfo.type == Card.Vibes.Hype))
            {
                cardsInTheMenu.Add(partyDeckCard);
               
            }

        }

        //Randomly Remove a card
        if (cardsInTheMenu.Count > 0)
        {
            Card randomSelection = cardsInTheMenu[Mathf.FloorToInt(Random.Range(0, cardsInTheMenu.Count))];
            if (Encounter.playerDeck.cardsInDeck.Contains(randomSelection)) Encounter.playerDeck.Remove(randomSelection);
            else if (Encounter.playerDiscard.cardsInDiscard.Contains(randomSelection)) Encounter.playerDiscard.Remove(randomSelection);
            else
            {
                Encounter.playerHand.Discard(randomSelection);
                Encounter.playerDiscard.Remove(randomSelection);

                randomSelection.cardGameObject.GetComponent<CardGUIEvents>().StartCoroutine("RemoveFromGame");

            }
        }
        Encounter.cardGameFSM.TransitionTo<Encounter.NPCTurnEnd>();

    }

    public void SelectCardToRemove()
    {
        if (CardGUIEvents.cardSelectedByPlayer != null && CardGUIEvents.cardSelectedByPlayer.cardGameObject.transform.parent == Encounter.menuGrid.transform)
        {
            if (Encounter.playerDeck.cardsInDeck.Contains(CardGUIEvents.cardSelectedByPlayer)) Encounter.playerDeck.Remove(CardGUIEvents.cardSelectedByPlayer);
            else if (Encounter.playerDiscard.cardsInDiscard.Contains(CardGUIEvents.cardSelectedByPlayer)) Encounter.playerDiscard.Remove(CardGUIEvents.cardSelectedByPlayer);
            else
            {
                Encounter.playerHand.Discard(CardGUIEvents.cardSelectedByPlayer);
                Encounter.playerDiscard.Remove(CardGUIEvents.cardSelectedByPlayer);
                Object.Destroy(CardGUIEvents.cardSelectedByPlayer.cardGameObject);

            }
            Encounter.menuGrid.transform.DetachChildren();
            CardGUIEvents.cardSelectedByPlayer = null;
            Encounter._selectACardMenu.SetActive(false);
            Encounter.cardDropZone.SetActive(true);
            Encounter.cardGameFSM.TransitionTo<Encounter.NPCTurnEnd>();
            return;
        }
       
        
        foreach (Card cardInMenu in cardsInTheMenu)
        {
            cardInMenu.cardGameObject.SetActive(false);
        }
        int numberOfCardsShown = (int)Mathf.Min(5, cardsInTheMenu.Count);
        Encounter.menuGrid.GetComponent<SelectCardFromMenu>().numberOfCardsShown = numberOfCardsShown;

        for (int i = 0; i < numberOfCardsShown; i++)
        {
            Encounter.menuGrid.GetComponent<SelectCardFromMenu>().cardsCurrentlyShown[i] = cardsInTheMenu[index + i].cardGameObject;
            cardsInTheMenu[index + i].cardGameObject.SetActive(true);
        }
        Encounter.menuGrid.GetComponent<SelectCardFromMenu>().UpdateCardsShown();

    }

}

