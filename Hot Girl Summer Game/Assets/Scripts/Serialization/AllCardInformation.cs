using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AllCardInformation", order = 1)]
public class AllCardInformation : ScriptableObject 
{

    public TextAsset cardSpreadsheet;
    public Dictionary<string, Card.CardInfo> cardInfoDictionary;


    public void ReadFromSpreadsheet()
	{
        cardInfoDictionary = CardParser.ReadMasterSpreadsheet(cardSpreadsheet);
	}
}

[CustomEditor(typeof(AllCardInformation))]
public class CardInformationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myTarget = (AllCardInformation)target;
        myTarget.cardSpreadsheet = (TextAsset) EditorGUILayout.ObjectField((Object)myTarget.cardSpreadsheet, typeof(TextAsset), false);
    }
}
