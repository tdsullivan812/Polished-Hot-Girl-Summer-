using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC
{
    public string npcName;
    public Sprite npcSprite;
    public Card selectedCard;

    public abstract void Effect();

}
