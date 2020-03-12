using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kelly : NPC
{
    private GameObject _selectACardMenu;
    private GameObject parentGameObject;
    public Kelly()
    {
        npcName = "Kelly";
        npcSprite = null;

       // _selectACardMenu = GameObject.Find("GridOfCards");
        //parentGameObject = GameObject.Find("SelectACard");
        //parentGameObject.SetActive(false);
    }


    public override void Effect()
    {
        /*
        Debug.Log("Kelly Effect");
        Encounter.WaitForInput.whatAmIWaitingFor = SelectCardToRemove;
        Encounter.cardGameFSM.TransitionTo<Encounter.WaitForInput>();
        parentGameObject.SetActive(true);
        foreach (Card partyDeckCard in GameController.partyDeck.allCards)
        {
            if ((partyDeckCard.displayedInfo.type == (int)Card.Vibes.Bubbly) || (partyDeckCard.displayedInfo.type == (int)Card.Vibes.Hype))
            {
                partyDeckCard.cardGameObject.transform.SetParent(_selectACardMenu.transform);
                partyDeckCard.cardGameObject.SetActive(true);
                var temporaryButton = partyDeckCard.cardGameObject.AddComponent<UnityEngine.UI.Button>();
                temporaryButton.onClick.AddListener(SelectThisCard);
            }
        }*/
    }

    public void SelectCardToRemove()
    {
        Card selectedCard;



    }

}

