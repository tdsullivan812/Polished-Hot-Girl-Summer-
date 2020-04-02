using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Compliment : Card
{
   public Compliment()
    {
        InitializeCardGameObject();
    }

    public override void Effect()
    {
        Encounter.WaitForInput.whatAmIWaitingFor = DuplicateCard;
        Encounter.cardGameFSM.TransitionTo<Encounter.WaitForInput>();
    }

    public void DuplicateCard()
    {
        if (CardGUIEvents.cardSelectedByPlayer != null)
        {

            
            Card duplicatedCard = (Card)CardGUIEvents.cardSelectedByPlayer.GetType().GetConstructor(Type.EmptyTypes).Invoke(null); //Finds constructor of the card and invokes it
            duplicatedCard.InitializeCardGameObject();
            Encounter.playerHand.AddToHand(duplicatedCard);
            CardGUIEvents.cardSelectedByPlayer = null;
            Encounter.cardGameFSM.TransitionTo<Encounter.PlayerTurn>();
        }
    }
}
