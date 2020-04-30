using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cody : NPC
{
    private int targetPoints;
    private Card.Vibes targetVibe;
    public Cody()
    {
        npcName = "Cody";
        npcSprite = null;
        victoryCondition = RequiredPoints;
        successMessage = "Cody Solved";
        failureMessage = "Cody Unsolved";
        turnsExpired = 0;

        if (GameController.partyDeck.victoryPoints.calmPoints < GameController.partyDeck.victoryPoints.hypePoints)
        {
            targetPoints = GameController.partyDeck.victoryPoints.calmPoints + 10;
            targetVibe = Card.Vibes.Calm;
        }
        else
        {
            targetPoints = GameController.partyDeck.victoryPoints.hypePoints + 10;
            targetVibe = Card.Vibes.Hype;
        }
    }

    public bool RequiredPoints()
    {
        if (targetVibe == Card.Vibes.Calm && GameController.partyDeck.victoryPoints.calmPoints >= targetPoints) return true;
        else if (targetVibe == Card.Vibes.Hype && GameController.partyDeck.victoryPoints.hypePoints >= targetPoints) return true;
        else return false;
    }

    public override void Effect()
    {
        List<Card> vpCards = new List<Card>();

        foreach(Card cardInDiscard in Encounter.playerDiscard.cardsInDiscard)
        {
            if (!cardInDiscard.displayedInfo.isPlayable)
            {
                vpCards.Add(cardInDiscard);
            }
        }
        if (vpCards.Count > 0)
        {
            Encounter.playerDiscard.RemoveFromDiscard(vpCards[UnityEngine.Random.Range(0, vpCards.Count)]);
        }
       

        Encounter.cardGameFSM.TransitionTo<Encounter.NPCTurnEnd>();
    }
}
