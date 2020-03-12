using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characteristic 
{
    public Sprite[] listOfSprites;

    public Sprite[] spritesOnDisplay = new Sprite[5];

    protected int firstSpriteIndex;
    protected int lastSpriteIndex;

    public void IncrementDisplay(bool isLeft)
    {
        int minimumNumberOfDisplayedOptions = Mathf.Min(5, listOfSprites.Length);
        spritesOnDisplay = new Sprite[minimumNumberOfDisplayedOptions];
        if (isLeft)
        {
            firstSpriteIndex = (firstSpriteIndex + 1) % listOfSprites.Length;
            lastSpriteIndex = (lastSpriteIndex + 1) % listOfSprites.Length;
            for (int i = 0; i < minimumNumberOfDisplayedOptions; i++)
            {
                Debug.Log(spritesOnDisplay[i]);
                Debug.Log(spritesOnDisplay.Length);
                Debug.Log(listOfSprites.Length);
                Debug.Log(lastSpriteIndex);
                spritesOnDisplay[i] = listOfSprites[(firstSpriteIndex + i) % listOfSprites.Length];
            }
        }

        else
        {
            firstSpriteIndex = (firstSpriteIndex + listOfSprites.Length - 1) % listOfSprites.Length;
            lastSpriteIndex = (lastSpriteIndex + listOfSprites.Length - 1) % listOfSprites.Length;
            for (int i = 0; i < minimumNumberOfDisplayedOptions; i++)
            {
                Debug.Log(spritesOnDisplay[i]);
                Debug.Log(spritesOnDisplay.Length);
                Debug.Log(listOfSprites.Length);
                Debug.Log(lastSpriteIndex);
                Debug.Log(firstSpriteIndex);
                spritesOnDisplay[i] = listOfSprites[(firstSpriteIndex + i) % listOfSprites.Length];
            }
        }

        CustomizableOptions.DisplayOptions(CustomizableOptions.currentlyDisplayed, CustomizableOptions.portraitRenderer.gameObject.tag);
    }
}
