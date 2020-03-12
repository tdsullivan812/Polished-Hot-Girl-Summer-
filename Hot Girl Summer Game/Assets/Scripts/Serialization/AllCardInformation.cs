using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AllCardInformation", order = 1)]
public class AllCardInformation : ScriptableObject 
{

    public readonly Dictionary<string, Card.CardInfo> cardInfoDictionary;


    public AllCardInformation(TextAsset cardSpreadsheet)
	{
        cardInfoDictionary = CardParser.ReadMasterSpreadsheet(cardSpreadsheet);
	}
}
