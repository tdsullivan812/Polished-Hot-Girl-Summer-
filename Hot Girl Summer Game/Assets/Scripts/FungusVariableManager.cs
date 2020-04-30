using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FungusVariableManager
{
    private static Fungus.Flowchart flowchart;

    private static Fungus.VariableReference _hype;
    private static Fungus.VariableReference _bubbly;
    private static Fungus.VariableReference _chill;
    private static Fungus.VariableReference _metKelly;
    private static Fungus.VariableReference _kellySolved;
    private static Fungus.VariableReference _metMeg;
    private static Fungus.VariableReference _megSolved;
    private static Fungus.VariableReference _metCori;
    private static Fungus.VariableReference _coriSolved;
    private static Fungus.VariableReference _metLeon;
    private static Fungus.VariableReference _leonSolved;
    private static Fungus.VariableReference _metJamie;
    private static Fungus.VariableReference _jamieSolved;
    private static Fungus.VariableReference _metCody;
    private static Fungus.VariableReference _codySolved;

    private static Fungus.VariableReference _coriProblem;
    private static Fungus.VariableReference _leonProblem;
    private static Fungus.VariableReference _codyProblem;

    public struct GlobalPartyVariables
    {
        public bool MetKelly;
        public bool KellySolved;

        public bool MetMeg;
        public bool MegSolved;

        public bool MetCody;
        public bool CodySolved;

        public bool MetCori;
        public bool CoriSolved;

        public bool MetLeon;
        public bool LeonSolved;

        public bool MetJamie;
        public bool JamieSolved;

        public float Chill;
        public float Hype;
        public float Bubbly;

        public int CoriProblem;
        public int LeonProblem;
        public int CodyProblem;

    }

    private static GlobalPartyVariables currentPartyState;

    public static void Initialize()
    {
        flowchart = Services.gameController.flowchart;

        _hype.variable = flowchart.GetVariable("Hype");
        _bubbly.variable = flowchart.GetVariable("Bubbly");
        _chill.variable = flowchart.GetVariable("Chill");
        _metKelly.variable = flowchart.GetVariable("MetKelly");
        _kellySolved.variable = flowchart.GetVariable("KellySolved");
        _metMeg.variable = flowchart.GetVariable("MetMeg");
        _megSolved.variable = flowchart.GetVariable("MegSolved");
        _metCori.variable = flowchart.GetVariable("MetCori");
        _coriSolved.variable = flowchart.GetVariable("CoriSolved");
        _metLeon.variable = flowchart.GetVariable("MetLeon");
        _leonSolved.variable = flowchart.GetVariable("LeonSolved");
        _metCody.variable = flowchart.GetVariable("MetCody");
        _codySolved.variable = flowchart.GetVariable("CodySolved");
        _metJamie.variable = flowchart.GetVariable("MetJamie");
        _jamieSolved.variable = flowchart.GetVariable("JamieSolved");
        _coriProblem.variable = flowchart.GetVariable("CoriProblem");
        _leonProblem.variable = flowchart.GetVariable("LeonProblem");
        _codyProblem.variable = flowchart.GetVariable("CodyProblem");

    }

    public static void Update()
    {
        _hype.Set<float>((float)GameController.partyDeck.victoryPoints.hypePoints);
        _bubbly.Set<float>((float)GameController.partyDeck.victoryPoints.bubblyPoints);
        _chill.Set<float>((float)GameController.partyDeck.victoryPoints.calmPoints);

        StoreFlowchartToStruct();
    }

    public static void StoreFlowchartToStruct()
    {
        currentPartyState.MetKelly = _metKelly.Get<bool>();
        currentPartyState.KellySolved = _kellySolved.Get<bool>();
        currentPartyState.MetMeg = _metMeg.Get<bool>();
        currentPartyState.MegSolved = _megSolved.Get<bool>();
        currentPartyState.MetCori = _metCori.Get<bool>();
        currentPartyState.CoriSolved = _coriSolved.Get<bool>();
        currentPartyState.MetLeon = _metLeon.Get<bool>();
        currentPartyState.LeonSolved = _leonSolved.Get<bool>();
        currentPartyState.MetJamie = _metJamie.Get<bool>();
        currentPartyState.JamieSolved = _jamieSolved.Get<bool>();
        currentPartyState.MetCody = _metCody.Get<bool>();
        currentPartyState.CodySolved = _codySolved.Get<bool>();
        //currentPartyState.Hype = (float)GameController.partyDeck.victoryPoints.hypePoints;
        //currentPartyState.Chill = (float)GameController.partyDeck.victoryPoints.calmPoints;
        //currentPartyState.Bubbly = (float)GameController.partyDeck.victoryPoints.bubblyPoints;
        currentPartyState.CoriProblem = _coriProblem.Get<int>();
        currentPartyState.LeonProblem = _leonProblem.Get<int>();
        currentPartyState.CodyProblem = _codyProblem.Get<int>();
    }

    public static void StoreStructToFlowchart()
    {
        //_hype.Set<float>(currentPartyState.Hype);
        //_chill.Set<float>(currentPartyState.Chill);
        //_bubbly.Set<float>(currentPartyState.Bubbly);

        _metKelly.Set<bool>(currentPartyState.MetKelly);
        _kellySolved.Set<bool>(currentPartyState.KellySolved);
        _metMeg.Set<bool>(currentPartyState.MetMeg);
        _megSolved.Set<bool>(currentPartyState.MegSolved);
        _metCori.Set<bool>(currentPartyState.MetCori);
        _coriSolved.Set<bool>(currentPartyState.CoriSolved);
        _metLeon.Set<bool>(currentPartyState.MetLeon);
        _leonSolved.Set<bool>(currentPartyState.LeonSolved);
        _metJamie.Set<bool>(currentPartyState.MetJamie);
        _jamieSolved.Set<bool>(currentPartyState.JamieSolved);
        _metCody.Set<bool>(currentPartyState.MetCody);
        _codySolved.Set<bool>(currentPartyState.CodySolved);
        _coriProblem.Set<int>(currentPartyState.CoriProblem);
        _leonProblem.Set<int>(currentPartyState.LeonProblem);
        _codyProblem.Set<int>(currentPartyState.CodyProblem);
    }
}
