using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leon : NPC
{
    private int danceCardsPlayed;
    public Leon()
    {
        npcName = "Leon";
        npcSprite = NPCSprites.arrayOfSprites[(int)NPCSprites.Parse(npcName)];
        victoryCondition = RequiredCards;
        successMessage = "Leon Solved";
        failureMessage = "Leon Unsolved";
        turnsExpired = 0;
        danceCardsPlayed = 0;
        Services.eventManager.Register<ActionCardPlayed>(CheckWhatCardWasPlayed);
    }

    public bool RequiredCards()
    {
        if (danceCardsPlayed >= 3)
        {
            Services.eventManager.Unregister<ActionCardPlayed>(CheckWhatCardWasPlayed);
            return true;
        }
        else return false;
    }

    public void CheckWhatCardWasPlayed(HotGirlEvent hotGirlEvent)
    {
        ActionCardPlayed actionTaken = (ActionCardPlayed)hotGirlEvent;
        if (actionTaken.cardPlayed.GetType() == typeof(Dance))
        {
            danceCardsPlayed++;
        }
    }
    public override void Effect()
    {
        Card leonEffectCard = new Bubbly();
        Encounter.playerDeck.AddToDeck(leonEffectCard, 0);

        Encounter.cardGameFSM.TransitionTo<Encounter.NPCTurnEnd>();
    }


}
