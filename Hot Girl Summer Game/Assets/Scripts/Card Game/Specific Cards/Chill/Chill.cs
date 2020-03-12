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
        InitializeCardGameObject();
    }

    public override void Effect()
    {

        //THOMAS HELP idk what to do here to add calm vibe point next turn
        Encounter.BeginningOfTurn.whatHappensAtBeginningOfTurn = AddOneCalm;
        Debug.Log("played Chill");
    }

    public void AddOneCalm()
    {
        Encounter.playerDiscard.AddToDiscard(new Calm());
        Encounter.BeginningOfTurn.whatHappensAtBeginningOfTurn = null;
    }
}
