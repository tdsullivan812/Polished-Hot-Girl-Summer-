using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private static int layerMask;
    public static GraphicRaycaster raycaster;
    public static EventSystem cardGameEventSystem;
    public static List<RaycastResult> results;
    public static List<GameObject> cardGameObjects;
    public static GameObject currentlySelected;
    public static RectTransform playableCardZone;



    // Start is called before the first frame update
    public static void Initialize()
    {
        
        layerMask = 1 << 8;
        results = new List<RaycastResult>();
        cardGameObjects = new List<GameObject>();
        currentlySelected = null;

    }

    public static void FindGameObjects()
    {
        raycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        cardGameEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        playableCardZone = GameObject.Find("PlayableCardZone").GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            PointerEventData pointerEvent = new PointerEventData(cardGameEventSystem);
            pointerEvent.position = Input.mousePosition;


            if (currentlySelected == null)
            {
                raycaster.Raycast(pointerEvent, results);
                Debug.Log("Raycast sent");

                if (results.Count > 0)
                {
                    //Debug.Log(results[0].gameObject.name);
                    foreach(RaycastResult hit in results)
                    {
                        if (hit.gameObject.layer == 8)
                        {
                            currentlySelected = hit.gameObject;
                            Debug.Log(currentlySelected.name);
                            break;
                        }
                    }
                    

                }
            }


            if (cardGameObjects.Contains(currentlySelected))
            {
                Debug.Log("Begin drag");
               currentlySelected.transform.SetPositionAndRotation(pointerEvent.position, Quaternion.identity);
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

    private void PlayCard()
    {
        if (Encounter.cardGameFSM.CurrentState.GetType() == typeof(Encounter.PlayerTurn))
        {

            if (playableCardZone.rect.Contains(playableCardZone.InverseTransformPoint(currentlySelected.transform.position)) && currentlySelected.GetComponent<CardIdentifier>().whichCardIsThis.displayedInfo.isPlayable)
            {
                Debug.Log("I'm gonna play a card");
                Encounter.playerHand.PlayFromHand(currentlySelected.GetComponent<CardIdentifier>().whichCardIsThis);
            }

        }
    }
}
