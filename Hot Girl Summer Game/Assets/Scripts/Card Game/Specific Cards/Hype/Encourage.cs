using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encourage : Card
{
    public Encourage()
    {
        /*
        displayedInfo.cardName = "Encourage";
        displayedInfo.type = (int)Vibes.Hype;
        displayedInfo.value = 1;
        displayedInfo.isPlayable = true;
        displayedInfo.text = "The nest time the other character takes an action, they take it twice.";
        displayedInfo.art = Resources.Load<Sprite>("");
        */
        InitializeCardGameObject();
    }

    public override void Effect()
    {
        Services.eventManager.Register<NPCTakesAction>(TakeActionAgain);
        Debug.Log("You are very cool.");        
        //tells game to have NPC go twice next turn
    }

    public void TakeActionAgain(HotGirlEvent npcTakesAction)
    {
        NPCTakesAction npcActionEvent = (NPCTakesAction)npcTakesAction;
        Services.eventManager.Unregister<NPCTakesAction>(TakeActionAgain);
        Services.encounter.OpponentEffect();
    }
}
