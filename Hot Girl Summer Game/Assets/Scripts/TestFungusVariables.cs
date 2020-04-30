using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFungusVariables : MonoBehaviour
{
    private Fungus.Flowchart flowchart;

    private Fungus.VariableReference hype;
    private Fungus.VariableReference bubbly;
    private Fungus.VariableReference chill;


    // Start is called before the first frame update
    void Start()
    {
        flowchart = Services.gameController.flowchart;

        hype.variable = flowchart.GetVariable("Hype");
        bubbly.variable = flowchart.GetVariable("Bubbly");
        chill.variable = flowchart.GetVariable("Chill");
    }

    // Update is called once per frame
    void Update()
    {
        hype.Set<float>((float)GameController.partyDeck.victoryPoints.hypePoints);
        bubbly.Set<float>((float)GameController.partyDeck.victoryPoints.bubblyPoints);
        chill.Set<float>((float)GameController.partyDeck.victoryPoints.calmPoints);
    }
}
