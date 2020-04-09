using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCards : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGetPumped()
    {
        GameController.partyDeck.AddCard(new Encourage());
        GameController.partyDeck.CalculateVictoryPoints();
    }

    public void AddPrivateTalk()
    {
        GameController.partyDeck.AddCard(new PrivateTalk());
        GameController.partyDeck.CalculateVictoryPoints();
    }

    public void AddEncourage()
    {
        GameController.partyDeck.AddCard(new Encourage());
        GameController.partyDeck.CalculateVictoryPoints();
    }

    public void AddChat()
    {
        GameController.partyDeck.AddCard(new Chat());
        GameController.partyDeck.CalculateVictoryPoints();
    }

    public void AddDance()
    {
        GameController.partyDeck.AddCard(new Dance());
        GameController.partyDeck.CalculateVictoryPoints();
    }

    public void AddJoke()
    {
        GameController.partyDeck.AddCard(new Joke());
        GameController.partyDeck.CalculateVictoryPoints();
    }

    public void AddChill()
    {
        GameController.partyDeck.AddCard(new Chill());
        GameController.partyDeck.CalculateVictoryPoints();
    }

    public void AddGutsy()
    {
        GameController.partyDeck.AddCard(new Gutsy());
        GameController.partyDeck.CalculateVictoryPoints();

    }

    public void AddCompliment()
    {
        GameController.partyDeck.AddCard(new Compliment());
        GameController.partyDeck.CalculateVictoryPoints();
    }
}
