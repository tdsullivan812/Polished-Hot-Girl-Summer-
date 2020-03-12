using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joke : Card
{
    public Joke()
    {
        /*
        displayedInfo.cardName = "Joke";
        displayedInfo.type = (int) Vibes.Bubbly;
        displayedInfo.value = 1;
        displayedInfo.isPlayable = true;
        displayedInfo.text = "Draw 1 card and take 1 more action this turn.";
        displayedInfo.art = Resources.Load<Sprite>("");
        */
        InitializeCardGameObject();
    }

    public override void Effect()
    {
        Encounter.playerDeck.Draw();
        Encounter.playerActions++;
    }
}
