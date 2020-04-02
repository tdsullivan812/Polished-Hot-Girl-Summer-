using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Score_CardGame : MonoBehaviour //not tested! :D
{
    private GameObject textObject; //where the score goes to
    private TextMeshProUGUI thisScoreboard; //tmp thingy
    public Card.VictoryPoints sbVictoryPoints; //victory points for scoreboard

    private int _sbTotal, _sbBubbly, _sbHype, _sbCalm; //scoreboard values

    void Start()
    {
        textObject = GameObject.Find("Scoreboard");//the scoreboard
        thisScoreboard = textObject.GetComponent<TextMeshProUGUI>(); 
    }

    // Update is called once per frame
    void Update()
    { 
        sbVictoryPoints = GameController.partyDeck.victoryPoints; //make scoreboard vp = deck vp

        //assign scoreboard values
        _sbTotal = sbVictoryPoints.totalPoints;
        _sbBubbly = sbVictoryPoints.bubblyPoints;
        _sbHype = sbVictoryPoints.hypePoints;
        _sbCalm = sbVictoryPoints.calmPoints;


        thisScoreboard.SetText("VP (Total): " + _sbTotal + " |\nBubbly: " + _sbBubbly +
            " |\nHype: " + _sbHype + " |\nCalm: " + _sbCalm); //update score
    }
}
