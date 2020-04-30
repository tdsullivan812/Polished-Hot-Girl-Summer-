using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meg : NPC
{
    List<Card> hypeCardsPlayed;

    public Meg()
    {
        npcName = "Meg";
        npcSprite = Services.gameController.arrayOfSprites[(int)NPCSprites.Parse(npcName)];
        victoryCondition = RequiredHypePoints;
        successMessage = "Meg Solved";
        failureMessage = "Meg Unsolved";
        turnsExpired = 0;
        description = "Get 16 or more Hype points to pump up Meg! Every turn, she will play your last Hype card a second time.";
        hypeCardsPlayed = new List<Card>();
        Services.eventManager.Register<ActionCardPlayed>(CheckWhatCardWasPlayed);
    }
    public override void Effect()
    {
        if (hypeCardsPlayed.Count > 0)
        {
            hypeCardsPlayed[hypeCardsPlayed.Count - 1].Effect();
            hypeCardsPlayed.Clear();
        }

        Encounter.cardGameFSM.TransitionTo<Encounter.NPCTurnEnd>();

    }

    public bool RequiredHypePoints()
    {
        if (GameController.partyDeck.victoryPoints.hypePoints >= 16)
        {
            return true;
        }
        else return false;
    }

    private void CheckWhatCardWasPlayed(HotGirlEvent hotGirlEvent)
    {
        ActionCardPlayed action = (ActionCardPlayed)hotGirlEvent;
        if(action.cardPlayed.displayedInfo.type == Card.Vibes.Hype)
        {
            hypeCardsPlayed.Add(action.cardPlayed);
        }

        
    }




}
