using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jamie : NPC
{
    public Jamie()
    {
        npcName = "Jamie";
        npcSprite = null;
        victoryCondition = RequiredRatio;
        successMessage = "Jamie Solved";
        failureMessage = "Jamie Unsolved";
        turnsExpired = 0;
    }

    public bool RequiredRatio()
    {
        if (GameController.partyDeck.victoryPoints.bubblyPoints >= 2 * Mathf.Min(GameController.partyDeck.victoryPoints.calmPoints, GameController.partyDeck.victoryPoints.hypePoints))
        {
            return true;
        }
        else return false;
    }

    //Looks at top 2 cards of deck and puts any Hype or Calm in the discard
    public override void Effect()
    {
        for (int i = 0; i < 2; i++)
        {
            if (Encounter.playerDeck.cardsInDeck.Count == 0)
            {
                return;
            }
            Card firstFromTop = Encounter.playerDeck.cardsInDeck[0];
            if (firstFromTop.displayedInfo.type != Card.Vibes.Bubbly)
            {
                Encounter.playerDiscard.AddToDiscard(Encounter.playerDeck.RemoveFromDeck(firstFromTop));
            }
        }

        Encounter.cardGameFSM.TransitionTo<Encounter.NPCTurnEnd>();

    }
}
