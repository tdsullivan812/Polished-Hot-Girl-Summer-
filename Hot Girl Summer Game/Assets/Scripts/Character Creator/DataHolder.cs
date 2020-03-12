using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class DataHolder
{
    public string[] Head;
    public string[] Hair;
    public string[] Eyes;
    public string[] Nose;
    public string[] Mouth;



    public static DataHolder CreateFromJSON(string JsonFile)
    {
        return JsonUtility.FromJson<DataHolder>(JsonFile);
    }


}
