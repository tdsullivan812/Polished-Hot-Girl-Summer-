using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CustomizableOptions : MonoBehaviour
{
    public string streamingPath;
    public static DataHolder featureSprites;
    public static Characteristic currentlyDisplayed;
    public static SpriteRenderer portraitRenderer;
    private static SpriteRenderer[] optionSprites;


    // Start is called before the first frame update
    void Awake()
    {
        featureSprites = new DataHolder();
        streamingPath = Path.Combine(Application.streamingAssetsPath, "CustomizableOptions.json");

        StreamReader reader = new StreamReader(streamingPath);

        string fileContents = reader.ReadToEnd();

        featureSprites = DataHolder.CreateFromJSON(fileContents);

        Debug.Log(featureSprites.Hair[0]);

        optionSprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    public static void DisplayOptions(Characteristic feature, string featureString) 
    {
        currentlyDisplayed = feature;
        portraitRenderer = GameObject.FindGameObjectWithTag(featureString).GetComponent<SpriteRenderer>();
        for (int i = 0; i < optionSprites.Length; i++)
        {
            Debug.Log(optionSprites[i]);
            Debug.Log(feature.spritesOnDisplay[i]);
            optionSprites[i].sprite = feature.spritesOnDisplay[i];
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
