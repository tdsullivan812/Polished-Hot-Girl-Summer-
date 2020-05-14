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

        }

        if (GameController.partyDeck.victoryPoints.hypePoints > GameController.partyDeck.victoryPoints.calmPoints && GameController.partyDeck.victoryPoints.hypePoints > GameController.partyDeck.victoryPoints.bubblyPoints)
        {
            vibeDisplay.text = "HYPE";
        }

        if (GameController.partyDeck.victoryPoints.bubblyPoints > GameController.partyDeck.victoryPoints.calmPoints  && GameController.partyDeck.victoryPoints.bubblyPoints > GameController.partyDeck.victoryPoints.hypePoints)
        {
            vibeDisplay.text = "BUBBLY";
        }

        bubblyVibes.text = GameController.partyDeck.victoryPoints.bubblyPoints.ToString();
        hypeVibes.text = GameController.partyDeck.victoryPoints.hypePoints.ToString();
        chillVibes.text = GameController.partyDeck.victoryPoints.hypePoints.ToString();
    }


}
