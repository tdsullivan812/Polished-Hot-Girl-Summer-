using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateTalk : Card

{
    public PrivateTalk()
    {
        /*
        displayedInfo.cardName = "Private Talk";
        displayedInfo.type = (int)Vibes.Calm;
        displayedInfo.value = 1;
        displayedInfo.isPlayable = true;
        displayedInfo.text = "Play your next action card twice. Add one Chill Victory Card to your discard.";
        displayedInfo.art = Resources.Load<Sprite>("");
        */

        InitializeCardGameObject();


    }
    public override void Effect()
    {
        var newCard = new Chill();
        Encounter.playerDiscard.AddToDiscard(newCard);
        //THOMAS PLS HELP ME ADD SOME CODE THAT ALLOWS YOU TO PLAY YOUR NEXT ACTION CARD TWICE.

        //Tells eventManager to execute PlayActionAgain when an action is played
        Services.eventManager.Register<ActionCardPlayed>(PlayActionAgain);

        Debug.Log("played Private Talk");
    }

    public void PlayActionAgain(HotGirlEvent aCardWasPlayed)
    {
        ActionCardPlayed actionCardEvent = (ActionCardPlayed)aCardWasPlayed;
        Services.eventManager.Unregister<ActionCardPlayed>(PlayActionAgain);
        Services.encounter.Play(actionCardEvent.cardPlayed);
    }
}
