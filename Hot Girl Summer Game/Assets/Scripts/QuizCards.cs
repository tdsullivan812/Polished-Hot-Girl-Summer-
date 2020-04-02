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
    }

    public void AddPrivateTalk()
    {
        GameController.partyDeck.AddCard(new PrivateTalk());
    }

    public void AddEncourage()
    {
        GameController.partyDeck.AddCard(new Encourage());
    }

    public void AddChat()
    {
        GameController.partyDeck.AddCard(new Chat());
    }

    public void AddDance()
    {
        GameController.partyDeck.AddCard(new Dance());
    }

    public void AddJoke()
    {
        GameController.partyDeck.AddCard(new Joke());
    }

    public void AddChill()
    {
        GameController.partyDeck.AddCard(new Chill());
    }

    public void AddGutsy()
    {
        GameController.partyDeck.AddCard(new Gutsy());

    }

    public void AddCompliment()
    {
    }
}
