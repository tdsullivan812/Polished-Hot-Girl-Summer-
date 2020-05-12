using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chill : Card
{
    public Chill()
    {
        /*
        displayedInfo.cardName = "Chill";
        displayedInfo.type = (int)Vibes.Calm;
        displayedInfo.value = 1;
        displayedInfo.isPlayable = true;
        displayedInfo.text = "Gain 1 Calm Vibe Point next turn.";
        displayedInfo.art = Resources.Load<Sprite>("");
        */
        //InitializeCardGameObject();
    }

    public override void Effect()
    {

        //Set the BeginningOfTurn delegate to the AddOneCalm method
        Encounter.BeginningOfTurn.whatHappensAtBeginningOfTurn = AddOneCalm;
    }

    //The card's Effect sets this method to execute at the beginning of the next turn
    public void AddOneCalm()
    {
        Encounter.playerDiscard.Add(new Calm()); //Adds one "Calm" to the player's discard
        Encounter.BeginningOfTurn.whatHappensAtBeginningOfTurn = null; //returns the BeginningOfTurn delegate to null
    }
}
