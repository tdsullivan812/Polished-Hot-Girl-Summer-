using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardIdentifier : MonoBehaviour
{
    public Card whichCardIsThis;

    public void SelectThisCard()
    {
        Encounter.npc.selectedCard = whichCardIsThis;

    }
}
