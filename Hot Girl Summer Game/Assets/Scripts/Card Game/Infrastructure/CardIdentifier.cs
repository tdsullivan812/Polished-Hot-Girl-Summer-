using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CardIdentifier : MonoBehaviour
{
    public Card whichCardIsThis;

    public void SelectThisCard()
    {
        Encounter.npc.selectedCard = whichCardIsThis;

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CardIdentifier))]
    public class CardIdentifierEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var mytarget = (CardIdentifier)target;
            EditorGUILayout.LabelField("Which Card is This: ", mytarget.whichCardIsThis.displayedInfo.cardName);
        }
    }
#endif
}
