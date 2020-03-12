using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : Characteristic
{

    public Mouth(Sprite[] sprites)
    {
        string[] listOfSpriteNames = CustomizableOptions.featureSprites.Mouth;
        listOfSprites = new Sprite[sprites.Length];
        listOfSprites = sprites;

        for (int i = 0; i < spritesOnDisplay.Length; i++)
        {
            if (i >= listOfSprites.Length) break;
            Debug.Log(listOfSpriteNames[i]);
            //listOfSprites[i] = Resources.Load<Sprite>("Hair / " + listOfSpriteNames[i]);
            spritesOnDisplay[i] = listOfSprites[i];
        }

        firstSpriteIndex = 0;
        lastSpriteIndex = Mathf.Min(listOfSprites.Length - 1, 4);

    }
}
