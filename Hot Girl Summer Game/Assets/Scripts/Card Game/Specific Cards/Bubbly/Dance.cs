using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : Card
{

    
    public Dance()
    {
        /*
        displayedInfo.cardName = "Dance";
        displayedInfo.type = Vibes.Bubbly;
        displayedInfo.value = 1;
        displayedInfo.isPlayable = true;
        displayedInfo.text = "Immediately add one Bubbly Victory Card to your discard pile.";
        displayedInfo.normalArt = Resources.Load<Sprite>("");
        */
        InitializeCardGameObject();
        
    }
    public override void Effect()
    {
        var newCard = new Bubbly();
        Encounter.playerDiscard.AddToDiscard(newCard);
    }
}
