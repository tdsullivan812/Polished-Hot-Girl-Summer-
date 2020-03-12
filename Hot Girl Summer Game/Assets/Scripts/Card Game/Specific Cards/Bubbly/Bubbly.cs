using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubbly : Card
{
    public Bubbly()
    {
        /*
        displayedInfo.cardName = "Bubbly";
        displayedInfo.type = Vibes.Bubbly;
        displayedInfo.value = 3;
        displayedInfo.isPlayable = false;
        displayedInfo.text = "Unplayable. Worth 3 Bubbly Points.";
        displayedInfo.normalArt = Resources.Load<Sprite>("Bubbly_Background");
        */
        InitializeCardGameObject();
    }

    public override void Effect()
    {

    }
}
