using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;

public class Icon : MonoBehaviour
{

    private BoxCollider2D iconCollider;
    private SpriteRenderer image;
    public string featureString;
    public Sprite[] sprites ; 
    public struct Features
    {
        public Characteristic featuredCharacteristic;
        //public Sprite[] listOfSprites;

        public Features(string featureString, Sprite[] sprites)
        {
            //listOfSprites = sprites;
            if (featureString == "Nose")
            {
                featuredCharacteristic = new Nose(sprites);
                
            }
            else if (featureString == "Head")
            {
                featuredCharacteristic = new Head(sprites);
            }
            else if (featureString == "Hair")
            {
                featuredCharacteristic = new Hair(sprites);
            }
            else if (featureString == "Eyes")
            {
                featuredCharacteristic = new Eyes(sprites);
            }
            else if (featureString == "Mouth")
            {
                featuredCharacteristic = new Mouth(sprites);
            }
            else featuredCharacteristic = null;

            
        }

        
    }
    public Features feature;


void Start()
    {
        iconCollider = gameObject.GetComponent<BoxCollider2D>();
        image = gameObject.GetComponent<SpriteRenderer>();
        feature = new Features(featureString, sprites);
        
    }

    private void OnMouseDown()
    {


        CustomizableOptions.DisplayOptions(feature.featuredCharacteristic, featureString);
    }

    private void OnMouseOver()
    {
        image.color = Color.yellow; 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
