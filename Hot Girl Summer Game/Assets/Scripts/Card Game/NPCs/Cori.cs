using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cori : NPC
{
    public Cori()
    {
        npcName = "Cori";
        npcSprite = null;
        victoryCondition = RequiredRatio;
        successMessage = "Cori Solved";
        failureMessage = "Cori Unsolved";
        turnsExpired = 0;
    }

    public bool RequiredRatio()
    {
        if (GameController.partyDeck.victoryPoints.hypePoints == GameController.partyDeck.victoryPoints.calmPoints)
        {
            return true;
        }
        else return false;
    }

    public override void Effect()
    {
        if (Encounter.playerDiscard.cardsInDiscard.Count > 0)
        {
            var index = UnityEngine.Random.Range(0, Encounter.playerDiscard.cardsInDiscard.Count);
            Card duplicatedCard = (Card)Encounter.playerDiscard.cardsInDiscard[index].GetType().GetConstructor(Type.EmptyTypes).Invoke(null);
            duplicatedCard.InitializeCardGameObject();
            Encounter.playerDiscard.AddToDiscard(duplicatedCard);
        }

        Encounter.cardGameFSM.TransitionTo<Encounter.NPCTurnEnd>();
    }
}
