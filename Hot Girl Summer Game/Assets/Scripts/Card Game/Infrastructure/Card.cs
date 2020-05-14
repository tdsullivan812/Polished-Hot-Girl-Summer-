using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public abstract class Card
{
    //Data Structures
    #region
    public struct CardInfo
    {
        public string cardName;
        public Vibes type;
        public int value;
        public bool isPlayable;
        public string text;
        public Sprite normalArt;
        public Sprite hoverArt;
        public VictoryPoints buyCost;

    }

    public struct VictoryPoints
    {
        public int calmPoints;
        public int bubblyPoints;
        public int hypePoints;
        public int totalPoints;
    }

    public enum Vibes : byte
    {
        Calm,
        Bubbly,
        Hype,
        Undefined
    }
    #endregion


    public GameObject cardGameObject;
    public CardInfo displayedInfo;
    //public DisplayedCard cardOnScreen;
    public abstract void Effect();

    public Card()
    {
        Debug.Log("card name is" + this.ToString());
        displayedInfo = Services.gameController.cardInfo.cardInfoDictionary[this.ToString()];
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 3 && Services.encounter != null)
        {
            InitializeCardGameObject();
        }
        //InitializeCardGameObject();
    }

    public void InitializeCardGameObject()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene() == UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(2))
        {
            cardGameObject = Object.Instantiate(Resources.Load<GameObject>("Cards/Basic Card"));

            //cardGameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].text = displayedInfo.cardName;
            TextMeshProUGUI textMesh = cardGameObject.GetComponentsInChildren<TextMeshProUGUI>(true)[1];
            textMesh.text = displayedInfo.text;
            textMesh.color = ChooseColor(displayedInfo.type);
            //cardGameObject.GetComponentsInChildren<TextMeshProUGUI>(true)[1].text = displayedInfo.text;
            //cardGameObject.GetComponentsInChildren<TextMeshProUGUI>(true)[1].outlineColor = ChooseColor(displayedInfo.type);

            var cardImages = cardGameObject.GetComponentsInChildren<UnityEngine.UI.Image>();
            //UnityEngine.UI.Image cardBackgroundImage = cardGameObject.GetComponent<UnityEngine.UI.Image>();
            //UnityEngine.UI.Image hoverImage = cardGameObject.GetComponentInChildren<UnityEngine.UI.Image>();
            cardGameObject.name = displayedInfo.cardName;
            cardImages[0].sprite = displayedInfo.normalArt;
            cardImages[1].sprite = displayedInfo.hoverArt;
            cardGameObject.AddComponent<CardIdentifier>().whichCardIsThis = this;
            return;
        }

        if (Encounter.objectPools.ContainsKey(displayedInfo.cardName) == false)
        {
            cardGameObject = Object.Instantiate(Resources.Load<GameObject>("Cards/Basic Card"));
            Encounter.objectPools.Add(this.displayedInfo.cardName, new ObjectPool(cardGameObject));

            //cardGameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].text = displayedInfo.cardName;
            TextMeshProUGUI textMesh = cardGameObject.GetComponentsInChildren<TextMeshProUGUI>(true)[1];
            textMesh.text = displayedInfo.text;
            textMesh.color = ChooseColor(displayedInfo.type);
            //cardGameObject.GetComponentsInChildren<TextMeshProUGUI>(true)[1].text = displayedInfo.text;
            //cardGameObject.GetComponentsInChildren<TextMeshProUGUI>(true)[1].outlineColor = ChooseColor(displayedInfo.type);

            var cardImages = cardGameObject.GetComponentsInChildren<UnityEngine.UI.Image>();
            //UnityEngine.UI.Image cardBackgroundImage = cardGameObject.GetComponent<UnityEngine.UI.Image>();
            //UnityEngine.UI.Image hoverImage = cardGameObject.GetComponentInChildren<UnityEngine.UI.Image>();
            cardGameObject.name = displayedInfo.cardName;
            cardImages[0].sprite = displayedInfo.normalArt;
            cardImages[1].sprite = displayedInfo.hoverArt;

            /*
            switch (displayedInfo.type)
            {
                case Card.Vibes.Calm:
                    cardBackgroundImage.sprite = Resources.Load<Sprite>("Cards/Bubbly_Background");
                    break;
                case Card.Vibes.Bubbly:
                    cardBackgroundImage.sprite = Resources.Load<Sprite>("Cards/Calm_Background");
                    break;
                case Card.Vibes.Hype:
                    cardBackgroundImage.sprite = Resources.Load<Sprite>("Cards/Hype_Background");
                    break;
                default:
                    break;

            }
            */
            cardGameObject.AddComponent<CardIdentifier>().whichCardIsThis = this;
            Encounter.objectPools[this.displayedInfo.cardName].Push(cardGameObject);
            //InputManager.activeCardGameObjects.Add(cardGameObject);
        }
    }

    public GameObject AssignGameObject()
    {
        //if (cardGameObject != null) return cardGameObject;
        string currentCardName = displayedInfo.cardName;
        GameObject cardObjectToAssign;
        if (Encounter.objectPools.ContainsKey(currentCardName) == false) //check if there is an existing pool for this card; if not, make one
        {
            InitializeCardGameObject();
            cardObjectToAssign = cardGameObject;
        }
        else if (Encounter.objectPools[currentCardName].Count == 0) //if there is a pool, check if there are any of this card in it; if not, instantiate a new object
        {
            cardObjectToAssign = Object.Instantiate(Encounter.objectPools[displayedInfo.cardName].cardGameObject, Encounter.objectPools[displayedInfo.cardName].cardGameObject.transform.parent);
            cardObjectToAssign.GetComponent<CardIdentifier>().whichCardIsThis = this;
        }
        else // if there is a card in the pool, just pop it
        {
            cardObjectToAssign = Encounter.objectPools[currentCardName].Pop();
        }
        cardObjectToAssign.GetComponent<CardGUIEvents>().StopAllCoroutines();
        cardGameObject = cardObjectToAssign;
        return cardObjectToAssign;
    }

    public static Vibes Parse(string vibeString)
    {
        if (vibeString == "Vibes.Bubbly") return Vibes.Bubbly;
        else if (vibeString == "Vibes.Calm") return Vibes.Calm;
        else if (vibeString == "Vibes.Hype") return Vibes.Hype;
        else return Vibes.Undefined;
    }

    private Color ChooseColor(Vibes vibe)
    {
        Color textColor;
        switch (vibe)
        {
            case Card.Vibes.Calm:
                textColor = new Color(12.0f/255, 56.0f/255, 54.0f/255, 0);
                break;
            case Card.Vibes.Bubbly:
                textColor = new Color(46.0f/255, 30.0f/255, 64.0f/255, 0);
                break;
            case Card.Vibes.Hype:
                textColor = new Color(76.0f/255, 8.0f/255, 44.0f/255, 0);
                break;
            default:
                textColor = Color.clear;
                break;

        }
        return textColor;
    }
}
