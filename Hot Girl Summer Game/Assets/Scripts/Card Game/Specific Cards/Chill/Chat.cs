using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat : Card
{
    public Chat()
    {
        /*
        displayedInfo.cardName = "Chat";
        displayedInfo.type = (int)Vibes.Calm;
        displayedInfo.value = 1;
        displayedInfo.isPlayable = true;
        displayedInfo.text = "Draw 2 cards.";
        displayedInfo.art = Resources.Load<Sprite>("");
        */
        InitializeCardGameObject();
    }
    public override void Effect()
    {
        Encounter.playerDeck.Draw();
        Encounter.playerDeck.Draw();

        Debug.Log("play Chat");
    }
}
