using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Encounter
{
    //Static elements in an encounter
    public static  Deck playerDeck;
    public static  Discard playerDiscard;
    public static Transform discardPileTransform;
    public static  Hand playerHand;
    public static bool _isItPlayerTurn;
    public static NPC npc;
    public static GameObject cardGUI;
    public static FiniteStateMachine<Encounter> cardGameFSM;
    public static GameObject endTurnButton;
    public static GameObject _selectACardMenu;
    public static GameObject    menuGrid;
    public static GameObject cardDropZone;

    //Constants for managing Hand GUI
    /*
    public const float beginningOfHand = -200;
    public const float endOfHand = 200;
    public const float handYPosition = -120;
    */

    //Remaining actions in a player turn
    public static int playerActions;

    public Encounter(NPC npc)
    {
        //Creates Deck, Hand, and Discard
        playerDeck = new Deck();
        playerDiscard = new Discard();
        playerHand = new Hand();


        Debug.Log("Created Hand, deck, discard");

        //Sets NPC for the encounter
        Encounter.npc = npc;

        //endTurnButton.SetActive(false);

        //initializes FSM
        cardGameFSM = new FiniteStateMachine<Encounter>(this);
        //cardGameFSM.TransitionTo<BeginningOfTurn>();
        Debug.Log("Transitioned to playerturn");

    }

    public void FindGameObjects()
    {
        if ((SceneManager.GetActiveScene() == SceneManager.GetSceneByName("CardEncounter")) && cardGUI == null)
        {
            //Finds GameObjects in scene
            cardGUI = GameObject.FindGameObjectWithTag("HandZone");
            endTurnButton = GameObject.Find("EndPlayerTurn");
            discardPileTransform = GameObject.Find("Discard Pile").transform;
            _selectACardMenu = GameObject.Find("Select A Card Menu");
            menuGrid = GameObject.Find("Card Zone");
            cardDropZone = GameObject.Find("PlayableCardZone");
            _selectACardMenu.SetActive(false);

        }
    }

    public void Play(Card cardToPlay)
    {
        var newEvent = new ActionCardPlayed(cardToPlay);
        Services.eventManager.Fire((HotGirlEvent)newEvent);

        cardToPlay.Effect();

        //Tells event manager that a card was played
        
    }

    public void ChangeTurn()
    {
        if (_isItPlayerTurn)
        {
            _isItPlayerTurn = false;
            cardGameFSM.TransitionTo<NPCTurnBegin>();
        }
        else
        {
            cardGameFSM.TransitionTo<BeginningOfTurn>();
        }
    }

    public void OpponentEffect()
    {
        npc.Effect();
        NPCTakesAction recentAction = new NPCTakesAction( npc);
        Services.eventManager.Fire((HotGirlEvent)recentAction);

    }
    //No longer being used
    #region
    //Called after changes to hand to make sure display represents actual cards in Hand
    public void UpdateHandSize()
    {

        //float handIncrement = (endOfHand - beginningOfHand) / (playerHand.handTransforms.Count + 1);
        //float xPosition = 0;
        /*
        foreach (RectTransform cardInHand in playerHand.handTransforms)
        {
            xPosition += handIncrement;
            cardInHand.SetPositionAndRotation(new Vector3(beginningOfHand + xPosition, handYPosition, 0), Quaternion.identity);
        }

    */
    }

    public void UpdateCardGameObjects()
    {
        /*
        for (int i = 0; i < playerHand.cardsInHand.Count; i++)
        {
            playerHand.cardsInHand[i].cardOnScreen.cardDisplay.transform.SetPositionAndRotation(playerHand.handTransforms[i].position, Quaternion.identity);
        } */
    }
    #endregion

    //Card Game Finite State Machine and States
    #region

    public class BeginningOfTurn : FiniteStateMachine<Encounter>.State
    {
        public delegate void BeginningOfTurnEffects();
        public static BeginningOfTurnEffects whatHappensAtBeginningOfTurn;

        public override void OnEnter()
        {
            //Any special beginning of turn effects happen here
            if (whatHappensAtBeginningOfTurn != null)
            {
                whatHappensAtBeginningOfTurn();

            }

            //These happen at the beginning of every turn
            foreach(Card card in Encounter.playerDeck.cardsInDeck)
            {
                Debug.Log(card);
            }

            _isItPlayerTurn = true;
            Encounter.playerActions = 1;
            playerDeck.Draw();
            playerDeck.Draw();
            playerDeck.Draw();
            playerDeck.Draw();

            Encounter.endTurnButton.SetActive(true);

            
        }

        public override void Update()
        {
            Encounter.cardGameFSM.TransitionTo<PlayerTurn>();
        }

    }

    public class PlayerTurn : FiniteStateMachine<Encounter>.State
    {
        public delegate void EndOfTurnEffects();
        public static EndOfTurnEffects whatHappensAtEndOfTurn;

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
            
            if (_isItPlayerTurn == false)
            {
                while (Encounter.playerHand.cardsInHand.Count > 0)
                {
                    Encounter.playerHand.Discard(Encounter.playerHand.cardsInHand[0]);
                }

                if (whatHappensAtEndOfTurn != null)
                {
                    whatHappensAtEndOfTurn();
                }
            }
            

        }

        public override void Update()
        {
            /*
            if (playerActions == 0)
            {
                endTurnButton.SetActive(true);
            }*/
        }

        
    }

    public class NPCTurnBegin : FiniteStateMachine<Encounter>.State
    {
        public override void OnEnter()
        {
            
            
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Update()
        {
            Services.encounter.OpponentEffect();
            //TransitionTo<NPCTurnEnd>();
        }
    }

    public class NPCTurnEnd : FiniteStateMachine<Encounter>.State
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
            npc.turnsExpired++;
            Services.encounter.ChangeTurn();
        }

    }

    public class WaitForInput : FiniteStateMachine<Encounter>.State
    {
        public delegate void CheckingForInput();
        public static CheckingForInput whatAmIWaitingFor;
        public override void OnEnter()
        {
            Debug.Log("waiting for input");
            CardGUIEvents.cardSelectedByPlayer = null;
            //Services.eventManager.Register<conditionToAdvance>(Encounter.cardGameFSM.StopWaitingForInput);
        }

        public override void OnExit()
        {
            foreach (Card card in Encounter.playerHand.cardsInHand)
            {
            }
        }

        public override void Update()
        {
            if (whatAmIWaitingFor != null)
            {
                whatAmIWaitingFor();

            }
        }
        

        //UNUSED for time being 
        #region
        public static void StopWaitingForInput(HotGirlEvent e)
        {
            cardGameFSM.TransitionTo<PlayerTurn>();
        }

        public void ExecuteDelegateFunction(CheckingForInput function)
        {
            function();
        }
        #endregion
    }

    #endregion
}
