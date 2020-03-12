using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card1Script : MonoBehaviour
{
    Text Card1;

    // Start is called before the first frame update
    void Start()
    {
        Card1 = GetComponent<Text>();
    }

    public void SlumberParty()
    {
        Card1.text = "Encourage";
    }

    public void TalkOut()
    {
        Card1.text = "Private Talk";

    }

    public void Hottie()
    {
        Card1.text = "Compliment";
    }
}
