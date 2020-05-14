using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager
{
    private static int layerMask;
    public static GraphicRaycaster cardGameRaycaster;
    public static GraphicRaycaster menuRaycaster;
    public static EventSystem cardGameEventSystem;
    public static List<RaycastResult> results;
    public static List<GameObject> activeCardGameObjects;
    public static GameObject currentlySelected;
    public static RectTransform playableCardZone;
    public static FiniteStateMachine<InputManager> inputFSM;
    public static Fungus.Clickable2D[] clickable2Ds;
    public static Camera camera;



    // Start is called before the first frame update
    public static void Initialize()
    {
        
        layerMask = 1 << 8;
        results = new List<RaycastResult>();
        //activeCardGameObjects = new List<GameObject>();
        currentlySelected = null;
        inputFSM = new FiniteStateMachine<InputManager>(Services.input);
       

    }

    public static void FindGameObjects()
    {
        cardGameRaycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        //menuRaycaster = GameObject.Find("MenuCanvas").GetComponent<GraphicRaycaster>();
        cardGameEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        playableCardZone = GameObject.Find("PlayableCardZone").GetComponent<RectTransform>();
        camera = GameObject.FindObjectOfType<Camera>();

    }
    // Update is called once per frame
    void Update()
    {
        
        CheckDragAndDrop();


    }

    private void PlayCard()
    {
        if (Encounter.cardGameFSM.CurrentState.GetType() == typeof(Encounter.PlayerTurn))
        {
            Debug.Log(currentlySelected.GetComponent<CardIdentifier>().whichCardIsThis);
            if (playableCardZone.rect.Contains(playableCardZone.InverseTransformPoint(currentlySelected.transform.position)) && currentlySelected.GetComponent<CardIdentifier>().whichCardIsThis.displayedInfo.isPlayable)
            {
                Debug.Log("I'm gonna play a card");
                Encounter.playerHand.PlayFromHand(currentlySelected.GetComponent<CardIdentifier>().whichCardIsThis);
            }

        }
    }

    public void CheckDragAndDrop()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            PointerEventData pointerEvent = new PointerEventData(cardGameEventSystem);
            pointerEvent.position = Input.mousePosition;


            if (currentlySelected == null)
            {
                cardGameRaycaster.Raycast(pointerEvent, results);
                Debug.Log("Raycast sent");

                if (results.Count > 0)
                {
                    Debug.Log(results[0].gameObject.name);
                    foreach (RaycastResult hit in results)
                    {
                        switch (hit.gameObject.layer)
                        {
                            case 8:
                                currentlySelected = hit.gameObject;

                                Debug.Log(currentlySelected.name);
                                break;
                            case 11:
                                hit.gameObject.GetComponent<MenuOfCards>().TriggerMenu();

                                break;
                            default:
                                break;

                        }
                        if (currentlySelected != null)
                        {
                            //Debug.Log("Begin drag");


                            break;
                        }
                        else continue; ;
                    }


                }
                results.Clear();
            }

            if (currentlySelected != null)
            {

                currentlySelected.transform.SetPositionAndRotation(currentlySelected.transform.InverseTransformVector(camera.ScreenToWorldPoint(pointerEvent.position)), Quaternion.identity);
            }
            



        }


        else if (currentlySelected != null)
        {
            if (Encounter.cardGameFSM.CurrentState.GetType() == typeof(Encounter.PlayerTurn) && Encounter.playerActions > 0)
            {
                PlayCard(); 
            }

            currentlySelected = null;
            results.Clear();
        }


    }

    public void DeactivateClickables()
    {
        for (int i = 0; i < clickable2Ds.Length; i++)
        {
            clickable2Ds[i].ClickEnabled = false;
        }
    }

    public void ReactivateClickables()
    {
        for (int i = 0; i < clickable2Ds.Length; i++)
        {
            clickable2Ds[i].ClickEnabled = true;
        }
    }
}

//Input Manager States begin here
#region


public class TitleScreen : FiniteStateMachine<InputManager>.State
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

public class CardEncounterPlayerTurn : FiniteStateMachine<InputManager>.State //The state where players can play cards and view their deck/discard
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
        Services.input.CheckDragAndDrop();
    }
}

public class CardEncounterSelectCard : FiniteStateMachine<InputManager>.State //The state where players are prompted to make a selection
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

public class CardEncounterMenu : FiniteStateMachine<InputManager>.State //The state where the player has opened a menu
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

public class CardEncounterTutorial : FiniteStateMachine<InputManager>.State //The state where the tutorial screen is shown
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

public class PartyMode : FiniteStateMachine<InputManager>.State //The state where the player is at the party
{
    public override void OnEnter()
    {
        InputManager.clickable2Ds = new Fungus.Clickable2D[10];
        InputManager.clickable2Ds = (Fungus.Clickable2D[])GameObject.FindObjectsOfType(typeof(Fungus.Clickable2D));
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {
        if (Services.gameController.flowchart.HasExecutingBlocks())
        {
            Context.DeactivateClickables();
        }
        else
        {
            Context.ReactivateClickables();
        }
    }


}
#endregion
