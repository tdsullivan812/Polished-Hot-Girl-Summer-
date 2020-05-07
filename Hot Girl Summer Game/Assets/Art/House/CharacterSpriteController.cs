using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteController : MonoBehaviour
{
    private GameObject hype, calm, bubbly;

    void Start()
    {
        hype = GameObject.Find("Application/View/InteractionBubbles/ib_hype");
        calm = GameObject.Find("Application/View/InteractionBubbles/ib_calm");
        bubbly = GameObject.Find("Application/View/InteractionBubbles/ib_bubbly");
    }

    void Update()
    {
        //check party state to determine which bubble to use & display that bubble
        //hype party
        if (GameController.partyDeck.victoryPoints.hypePoints > GameController.partyDeck.victoryPoints.bubblyPoints && GameController.partyDeck.victoryPoints.hypePoints > GameController.partyDeck.victoryPoints.calmPoints) 
        {
            hype.SetActive(true);

            calm.SetActive(false);
            bubbly.SetActive(false);
        }
        //calm party
        else if (GameController.partyDeck.victoryPoints.calmPoints > GameController.partyDeck.victoryPoints.bubblyPoints && GameController.partyDeck.victoryPoints.calmPoints > GameController.partyDeck.victoryPoints.hypePoints)
        {
            calm.SetActive(true);

            hype.SetActive(false);
            bubbly.SetActive(false);
        }
        //bubbly party
        else
        {
            bubbly.SetActive(true);

            hype.SetActive(false);
            calm.SetActive(false);
        }

    }

}

