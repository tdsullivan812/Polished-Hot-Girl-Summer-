using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NPCSprites", order = 2)]
public class NPCSprites : ScriptableObject
{
    public enum NPCIndex : int
    {
        Undefined = -1,
        Kelly = 0,
        Meg = 1,
        Cori = 2,
        Jamie = 3,
        Leon = 4,
        Cody = 5
    }

    public static NPCIndex Parse(string npcName)
    {
        if (npcName.ToUpper() == "KELLY") return NPCIndex.Kelly;
        else if (npcName.ToUpper() == "MEG") return NPCIndex.Meg;
        else if (npcName.ToUpper() == "CORI") return NPCIndex.Cori;
        else if (npcName.ToUpper() == "JAMIE") return NPCIndex.Jamie;
        else if (npcName.ToUpper() == "LEON") return NPCIndex.Leon;
        else if (npcName.ToUpper() == "CODY") return NPCIndex.Cody;
        else return NPCIndex.Undefined;

    }
    public static Sprite[] arrayOfSprites = new Sprite[6];

#if UNITY_EDITOR
    [CanEditMultipleObjects]
    [CustomEditor(typeof(NPCSprites))]
    public class CardInformationEditor : Editor
    {
        //CanEditMultipleObjects = true;
        public override void OnInspectorGUI()
        {

            NPCSprites.arrayOfSprites[0] = (Sprite)EditorGUILayout.ObjectField((Object)arrayOfSprites[0], typeof(Sprite), false);
            NPCSprites.arrayOfSprites[1] = (Sprite)EditorGUILayout.ObjectField((Object)arrayOfSprites[1], typeof(Sprite), false);
            NPCSprites.arrayOfSprites[2] = (Sprite)EditorGUILayout.ObjectField((Object)arrayOfSprites[2], typeof(Sprite), false);
            NPCSprites.arrayOfSprites[3] = (Sprite)EditorGUILayout.ObjectField((Object)arrayOfSprites[3], typeof(Sprite), false);
            NPCSprites.arrayOfSprites[4] = (Sprite)EditorGUILayout.ObjectField((Object)arrayOfSprites[4], typeof(Sprite), false);
            NPCSprites.arrayOfSprites[5] = (Sprite)EditorGUILayout.ObjectField((Object)arrayOfSprites[5], typeof(Sprite), false);
            EditorUtility.SetDirty(target);

        }


    }
#endif
}
