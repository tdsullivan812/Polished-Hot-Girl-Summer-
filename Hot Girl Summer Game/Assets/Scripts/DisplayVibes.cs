using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayVibes : MonoBehaviour
{
    public UnityEngine.UI.Text vibeDisplay;
    public UnityEngine.UI.Text vibeDescription;
    public UnityEngine.UI.Text bubblyVibes;
    public UnityEngine.UI.Text chillVibes;
    public UnityEngine.UI.Text hypeVibes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.partyDeck.victoryPoints.calmPoints > GameController.partyDeck.victoryPoints.hypePoints && GameController.partyDeck.victoryPoints.calmPoints > GameController.partyDeck.victoryPoints.bubblyPoints)
        {
            vibeDisplay.text = "CALM";
            vibeDescription.text = "The party is relaxed. The guests are enjoying the chance to talk to their friends without pressure.";

        }

        if (GameController.partyDeck.victoryPoints.hypePoints > GameController.partyDeck.victoryPoints.calmPoints && GameController.partyDeck.victoryPoints.hypePoints > GameController.partyDeck.victoryPoints.bubblyPoints)
        {
            vibeDisplay.text = "HYPE";
            vibeDescription.text = "The party is killer! The air is electric with youth, daring, and maybe a bit of misbehaviour.";
        }

        if (GameController.partyDeck.victoryPoints.bubblyPoints > GameController.partyDeck.victoryPoints.calmPoints  && GameController.partyDeck.victoryPoints.bubblyPoints > GameController.partyDeck.victoryPoints.hypePoints)
        {
            vibeDisplay.text = "BUBBLY";
            vibeDescription.text = "The party is a blast! Everyone is laughing and showing off their best dance moves.";
        }

        bubblyVibes.text = GameController.partyDeck.victoryPoints.bubblyPoints.ToString();
        hypeVibes.text = GameController.partyDeck.victoryPoints.hypePoints.ToString();
        chillVibes.text = GameController.partyDeck.victoryPoints.hypePoints.ToString();
    }


}
