﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameController : MonoBehaviour
{
    /********************************************************************************************

        This GameController is a Monobehaviour that should be present in all scenes in the game.
        It is in charge of maintaining global variables and the overall state of the game.
        It has a DontDestroyOnLoad command so that it can stay constant between scenes.

    ********************************************************************************************/

    // This is the game's Finite State Machine, which keeps track of which phase you are in. You can
    // see what the phases for the game are towards the bottom of this file. For now,
    // the only phases that matter are the CardGame phase and the Story phase, which it will 
    // switch between when playing cards and talking to characters, respectively. 
    private static FiniteStateMachine<GameController> _gameFSM;

    public AllCardInformation cardInfo;
    //public static NPCSprites listOfSprites;
    public Sprite[] arrayOfSprites;
    
    //This is the Party Deck, which is a list of all the cards the player owns. This is different from
    // the Deck object in an Encounter, because the Party Deck includes the discard pile.
    public static DeckList partyDeck;

    
    //This is the next NPC that the player will have a card Encounter with. For now, this should only be Kelly,
    // but in the future, when the player finishes one Encounter, the next NPC will be queued up.
    public static NPC nextNPC;

    //This is the next Fungus block that will execute when the game returns to party mode

    public string nextBlock;
    //This is the spreadsheet of all of the cards and their identifying info
    public TextAsset cardSpreadsheet;

    //A bool tracking whether the player has had a card encounter yet
    public bool firstTime = true;

    //The GameObject with the Fungus Flowchart;
    public GameObject flowchartGameObject;

    public Fungus.Flowchart flowchart;

    //public static Dictionary<string, ObjectPool> objectPools;

    // This is called even before the Start function. I just wanted to perform the DontDestroyOnLoad
    // as early as possible.
    private void Awake()
    {

        //The Services class is just a static class that acts as a list of all the major systems in the game.
        //Right now, Services has three attributes: the gameController (this), the eventManager, and the
        // encounter. 
        if (Services.gameController == null)
        {
            Object.DontDestroyOnLoad(this);
            /*
            flowchartGameObject = GameObject.Find("Flowchart");
            Object.DontDestroyOnLoad(flowchartGameObject);
            flowchart = flowchartGameObject.GetComponent<Fungus.Flowchart>();
            */
            cardInfo.ReadFromSpreadsheet();


            Services.gameController = this;
            Services.eventManager = new EventManager();
            Services.input = new InputManager();
            InputManager.Initialize();
            //Services.allCardInformation = AllCardInformation(cardSpreadsheet);



            //These lines initialize the public attributes in the GameController
            partyDeck = new DeckList();
            //objectPools = new Dictionary<string, ObjectPool>();

            nextNPC = new Kelly();

            //The gameController state machine is private, because this should be pretty much 
            //the only script changing the game state at such a high level.
            _gameFSM = new FiniteStateMachine<GameController>(this);

            partyDeck.AddCard(new Dance());
            partyDeck.AddCard(new Chill());
            partyDeck.AddCard(new Gutsy());

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0:
                    _gameFSM.TransitionTo<StartMenu>();
                    InputManager.inputFSM.TransitionTo<TitleScreen>();
                    break;

                case 1:
                    _gameFSM.TransitionTo<PersonalityQuiz>();
                    InputManager.inputFSM.TransitionTo<TitleScreen>();
                    break;

                case 2:
                    _gameFSM.TransitionTo<Story>();
                    //InputManager.inputFSM.TransitionTo<PartyMode>();
                    partyDeck.AddCard(new Bubbly());              //These lines add basic cards to the deck
                    partyDeck.AddCard(new Dance());
                    partyDeck.AddCard(new Gutsy());
                    partyDeck.AddCard(new PrivateTalk());
                    partyDeck.AddCard(new Chat());
                    partyDeck.AddCard(new Encourage());
                    partyDeck.CalculateVictoryPoints();
                    break;

                case 3:
                    _gameFSM.TransitionTo<CardGame>();
                    partyDeck.AddCard(new Bubbly());              //These lines add basic cards to the deck
                    partyDeck.AddCard(new Dance());
                    partyDeck.AddCard(new Gutsy());
                    partyDeck.AddCard(new PrivateTalk());
                    partyDeck.AddCard(new Chat());
                    partyDeck.AddCard(new Encourage());
                    partyDeck.CalculateVictoryPoints();
                    break;

                default:
                    break;

            }



           
            //PURELY FOR TESTING THE CARD ENCOUNTER SCENE. In the real game, we don't want to start playing
            // cards right away. Feel free to uncomment if you want to test the card game, but be mindful.

            #region

            
            
            




            //_gameFSM = new FiniteStateMachine<GameController>(this); //This line creates a state machine for 
            //_gameFSM.TransitionTo<CardGame>();

            #endregion


        }

        else this.enabled = false;
    }

    
    // Here is where important public variables should be initialized. Be careful if you are testing
    // individual scenes without running the game from the beginning; since the GameController normall 
    //stays constant between scenes, you may need to write a special case to make sure the scene gets
    //initialized properly.

    void Start()
    {
       


        //_gameFSM.TransitionTo<Story>();

    }

    // Update is called once per frame
    void Update()
    {
        //We call the Finite State Machines' updates because we want the game to do different things
        // each frame depending on the game state.

        
        //The Game Controller updates first
        _gameFSM.Update();
        InputManager.inputFSM.Update();

        
    }

    //SASHA'S PAUSE MENU ADDITIONS
    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }

    //END OF SASHA'S ADDITIONS

    

    public void SetNPC(string npcName)
    {
        if (npcName == "Kelly")
        {
            nextNPC = new Kelly();
        }

        else if (npcName == "Leon")
        {
            nextNPC = new Leon();
        }

        else if (npcName == "Meg")
        {
            nextNPC = new Meg();
        }

        else if (npcName == "Cori")
        {
            nextNPC = new Cori();
        }

        else if (npcName == "Cody")
        {
            nextNPC = new Cody();
        }

        else if (npcName == "Jamie")
        {
            nextNPC = new Jamie();
        }
    }

    public void LoadQuiz()
    {
        SceneManager.LoadScene(1);
        _gameFSM.TransitionTo<PersonalityQuiz>();
    }

    public void LoadParty()
    {
        SceneManager.LoadScene(2);
        _gameFSM.TransitionTo<Story>();
    }

    public void LoadCardGame()
    {
        SceneManager.LoadScene(3);
        _gameFSM.TransitionTo<CardGame>();
    }

    public string EvaluatePartyState()
    {
        if (Encounter.npc != null)
        {
            string partyState;
            if (Encounter.npc.victoryCondition())
            {
                partyState = Encounter.npc.successMessage;
            }
            else partyState = Encounter.npc.failureMessage;
            return partyState;
        }

        else return null;
    }

    public void ExecuteFungusBlock()
    {
        flowchartGameObject.GetComponent<Fungus.Flowchart>().FindBlock(EvaluatePartyState());
    }
    
    public void GetPartyFlowchart()
    {
        flowchartGameObject = GameObject.FindGameObjectWithTag("Flowchart");

        if (flowchart == null)
        {
            flowchart = flowchartGameObject.GetComponent<Fungus.Flowchart>();
            FungusVariableManager.Initialize();
            FungusVariableManager.StoreStructToFlowchart();
        }
        else
        {
            GameObject.Destroy(flowchartGameObject);
        }
        
    }
}

//GameControllerInspector
#region
#if UNITY_EDITOR
[CustomEditor(typeof(GameController))]
    public class GCInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var myTarget = (GameController)target;
        DrawDefaultInspector();
        myTarget.cardInfo = (AllCardInformation)EditorGUILayout.ObjectField((Object)myTarget.cardInfo, typeof(AllCardInformation), false);

    }
}
#endif

#endregion
// This is where the States for the Game Controller Finite State Machine are.
#region

public class StartMenu : FiniteStateMachine<GameController>.State
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {
        base.Update();
    }
}

public class PersonalityQuiz : FiniteStateMachine<GameController>.State
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {
        if (GameController.partyDeck.allCards.Count == 7) Services.gameController.LoadParty();
    }
}
//The state of the game during a Card Game. You shouldn't have to edit any of this.
//The important thing is that an Encounter is created when you enter this state.
public class CardGame : FiniteStateMachine<GameController>.State
{
    public override void OnEnter()
    {
        
        Services.encounter = new Encounter(GameController.nextNPC);
        Services.encounter.FindGameObjects();
        foreach(Card cardInDeck in GameController.partyDeck.allCards)
        {
            cardInDeck.InitializeCardGameObject();
        }
        Encounter.NPCDescription.GetComponent<TextMeshProUGUI>().text = Encounter.npc.description;
        Encounter.NPCPortrait.GetComponent<Image>().sprite = Encounter.npc.npcSprite;
        Encounter.deckZone.GetComponent<MenuOfCards>().Initialize();
        Encounter.discardZone.GetComponent<MenuOfCards>().Initialize();
        Encounter.cardGameFSM.TransitionTo<Encounter.BeginEncounter>();
        if (Services.gameController.firstTime)
        {
            InputManager.inputFSM.TransitionTo<CardEncounterTutorial>();
        }
        else InputManager.inputFSM.TransitionTo<CardEncounterPlayerTurn>();
    }

    public override void OnExit()
    {
        
        
    }

    public override void Update()
    {
        
        //Make sure the party deck is up to date
        GameController.partyDeck.UpdateContents();
        GameController.partyDeck.CalculateVictoryPoints();

        //Now, check if Victory Point conditions are met
        if (GameController.nextNPC.victoryCondition() || Encounter.npc.turnsExpired >= 10)
        {
            Services.gameController.nextBlock = Services.gameController.EvaluatePartyState();
            //If you have enough VP or time limit expires, the encounter ends
            //InputManager.inputFSM.TransitionTo<PartyMode>();
            TransitionTo<LoadingPartyScene>();
            
        }

        //If not, then the card Game Updates
        Encounter.cardGameFSM.Update();
        
    }
}

public class LoadingPartyScene : FiniteStateMachine<GameController>.State
{
    public override void OnEnter()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("SampleScene"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public override void Update()
    {
        TransitionTo<Story>();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

}
//The state of the game when you are just talking to party guests. You also shouldn't have to edit this.
public class Story : FiniteStateMachine<GameController>.State
{
    public override void OnEnter()
    {

        Services.gameController.GetPartyFlowchart();

        Services.gameController.nextBlock = Services.gameController.EvaluatePartyState();
        if (Services.gameController.nextBlock != null)
        {
            Fungus.BlockReference blockToExecute;
            blockToExecute.block = Services.gameController.flowchart.FindBlock(Services.gameController.nextBlock);
            //Debug.Log(blockToExecute.block.BlockName);
            //blockToExecute.block.Execute();
            Services.gameController.flowchart.ExecuteBlock(blockToExecute.block);
            Services.encounter = null;
        }

        InputManager.inputFSM.TransitionTo<PartyMode>();

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {


        FungusVariableManager.Update();
    }
}

#endregion
