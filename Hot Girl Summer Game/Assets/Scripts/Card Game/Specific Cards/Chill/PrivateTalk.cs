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

        //InitializeCardGameObject();


    }
    public override void Effect()
    {
        var newCard = new Chill();
        Encounter.playerDiscard.Add(newCard);

        //Tells eventManager to execute PlayActionAgain when an action is played
        Services.eventManager.Register<ActionCardPlayed>(PlayActionAgain);

        Debug.Log("played Private Talk");
    }

    //The Effect registers this method for the ActionCardPlayed event
    public void PlayActionAgain(HotGirlEvent aCardWasPlayed)
    {
        ActionCardPlayed actionCardEvent = (ActionCardPlayed)aCardWasPlayed;
        Services.eventManager.Unregister<ActionCardPlayed>(PlayActionAgain); //Unregisters this method
        Services.encounter.Play(actionCardEvent.cardPlayed); //Plays the card a second time
    }
}
