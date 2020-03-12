using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calm : Card
{
    public Calm()
    {
        /*
        displayedInfo.cardName = "Calm";
        displayedInfo.type = (int)Vibes.Calm;
        displayedInfo.value = 3;
        displayedInfo.isPlayable = false;
        displayedInfo.text = "Unplayable. Worth 3 Calm Points.";
        displayedInfo.art = Resources.Load<Sprite>("");
        */
        InitializeCardGameObject();
    }

    public override void Effect()
    {

    }
}
