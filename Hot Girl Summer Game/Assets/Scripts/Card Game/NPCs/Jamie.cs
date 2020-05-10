using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jamie : NPC
{
    public Jamie()
    {
        npcName = "Jamie";
        npcSprite = Services.gameController.arrayOfSprites[(int)NPCSprites.Parse(npcName)];
        victoryCondition = RequiredRatio;
        successMessage = "Jamie Solved";
        failureMessage = "Jamie Unsolved";
        turnsExpired = 0;
        description = "Have twice as much Bubbly Vibes as either Hype or Calm to make Jamie's night! If you have Hype or Calm cards on the top 2 cards of your deck, they will discard them each turn.";
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
                Encounter.playerDiscard.Add(Encounter.playerDeck.Remove(firstFromTop));
            }
        }

        Encounter.cardGameFSM.TransitionTo<Encounter.NPCTurnEnd>();

    }
}
