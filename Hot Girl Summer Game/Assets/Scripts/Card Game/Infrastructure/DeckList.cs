using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckList
{
    public List<Card> allCards;
    public Card.VictoryPoints victoryPoints;

    public DeckList()
    {
        allCards = new List<Card>();
        victoryPoints.bubblyPoints = 0;
        victoryPoints.calmPoints = 0;
        victoryPoints.hypePoints = 0;
        victoryPoints.totalPoints = 0;
        
    }
    public Card AddCard(Card cardToAdd)
    {
        allCards.Add(cardToAdd);
        return cardToAdd;
    }

    public Card RemoveCard(Card cardToRemove)
    {
        allCards.Remove(cardToRemove);
        return cardToRemove;
    }

    public void UpdateContents()
    {
        allCards.Clear();
        allCards.AddRange(Encounter.playerDeck.cardsInDeck);
        allCards.AddRange(Encounter.playerDiscard.cardsInDiscard);
        allCards.AddRange(Encounter.playerHand.cardsInHand);
    }

    public void CalculateVictoryPoints()
    {
        victoryPoints.bubblyPoints = 0;
        victoryPoints.calmPoints = 0;
        victoryPoints.hypePoints = 0;
        victoryPoints.totalPoints = 0;
        foreach (Card partyCard in allCards)
        {
            switch (partyCard.displayedInfo.type)
            {
                case Card.Vibes.Bubbly:
                    victoryPoints.bubblyPoints += partyCard.displayedInfo.value;
                    break;
                case Card.Vibes.Calm:
                    victoryPoints.calmPoints += partyCard.displayedInfo.value;
                    break;
                case Card.Vibes.Hype:
                    victoryPoints.hypePoints += partyCard.displayedInfo.value;
                    break;
                default:
                    break;
            }
        }

        victoryPoints.totalPoints = victoryPoints.bubblyPoints + victoryPoints.calmPoints + victoryPoints.hypePoints;
    }
}
