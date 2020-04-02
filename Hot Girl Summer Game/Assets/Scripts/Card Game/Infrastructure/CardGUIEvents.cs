using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardGUIEvents : EventTrigger
{

    private Vector3 startingPosition;
    public static RectTransform playableCardZone;
    public static Card cardSelectedByPlayer;
    private static float sizeWhenHovering = 1;
    // Start is called before the first frame update
    void Start()
    {
        playableCardZone = GameObject.Find("PlayableCardZone").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region

    public override void OnPointerClick(PointerEventData eventData)
    {
        if(Encounter.cardGameFSM.CurrentState.GetType() == typeof(Encounter.WaitForInput))
        {
            cardSelectedByPlayer = GetComponent<CardIdentifier>().whichCardIsThis;
        }
    }
    public override void OnPointerEnter(PointerEventData pointerEvent)
    {
        Debug.Log("Hovering over card");
        StopCoroutine("StopHoverEffect");
        StartCoroutine("HoverEffect");
    }


    public override void OnPointerExit(PointerEventData pointerEvent)
    {
        Debug.Log("No longer hovering");
        StopCoroutine("HoverEffect");
        StartCoroutine("StopHoverEffect");
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        startingPosition = gameObject.transform.localPosition;
    }
    public override void OnDrag(PointerEventData pointerEvent)
    {

        gameObject.transform.SetPositionAndRotation(pointerEvent.position, Quaternion.identity);

    }


    public override void OnEndDrag(PointerEventData pointerEvent)
    {
        Debug.Log("No longer dragging");
        Debug.Log(Encounter.cardGameFSM.CurrentState.GetType());

        //What to do if it's the player's main phase in their turn
        if (Encounter.cardGameFSM.CurrentState.GetType() == typeof(Encounter.PlayerTurn))
        {
            Debug.Log(gameObject.transform.localPosition);
            Debug.Assert(playableCardZone.rect.Contains(gameObject.transform.localPosition), "Assertion failed: Rect contains GameObject");
            Debug.Assert(GetComponent<CardIdentifier>().whichCardIsThis.displayedInfo.isPlayable, "Assertion failed: Card is playable");

            Debug.Log(pointerEvent.position);
            Debug.Log(playableCardZone.InverseTransformPoint(pointerEvent.position));
            if (playableCardZone.rect.Contains(playableCardZone.InverseTransformPoint(pointerEvent.position)) && GetComponent<CardIdentifier>().whichCardIsThis.displayedInfo.isPlayable)
            {
                Debug.Log("I'm gonna play a card");
                Encounter.playerHand.PlayFromHand(GetComponent<CardIdentifier>().whichCardIsThis);
            }
            else
            {
                gameObject.transform.SetPositionAndRotation(startingPosition, Quaternion.identity);
            }
        }
        
        //What to do if the game is waiting for players to pick a card
        //else if (Encounter.cardGameFSM.CurrentState.GetType() == typeof(Encounter.WaitForInput))
        //{
        //    if (playableCardZone.rect.Contains(playableCardZone.InverseTransformPoint(pointerEvent.position)))
        //    {
        //        cardSelectedByPlayer = GetComponent<CardIdentifier>().whichCardIsThis;
        //    }
        //    else
        //    {
        //        gameObject.transform.SetPositionAndRotation(startingPosition.position, Quaternion.identity);
        //    }
        //}
    }

    #endregion

    private IEnumerator HoverEffect()
    {
        float parameter = 0;
        //Color currentImageAlpha = thisCard.GetComponentsInChildren<UnityEngine.UI.Image>()[0].color;
        //Color newImageAlpha = thisCard.GetComponentsInChildren<UnityEngine.UI.Image>()[1].color;
        Color transparent = new Color(1, 1, 1, 0);
        Color currentTextColor = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().color;
        Color targetTextColor = new Color(currentTextColor.r, currentTextColor.g, currentTextColor.b, 1);
        while (parameter < 1)
        {
            gameObject.GetComponentsInChildren<UnityEngine.UI.Image>()[0].color = Color.Lerp(Color.white, transparent, parameter);
            gameObject.GetComponentsInChildren<UnityEngine.UI.Image>()[1].color = Color.Lerp(transparent, Color.white, parameter);
            //gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.Lerp(transparent, Color.white, parameter);
            gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.Lerp(currentTextColor, targetTextColor, parameter);
            //gameObject.transform.localScale.Set(Mathf.Lerp(1, sizeWhenHovering, parameter), Mathf.Lerp(1, sizeWhenHovering, parameter), 1);

            parameter += 0.01f;
            yield return null;
        }
        yield return null;
    }

    private IEnumerator StopHoverEffect()
    {
        float parameter = 0;
        //Color currentImageAlpha = thisCard.GetComponentsInChildren<UnityEngine.UI.Image>()[0].color;
        //Color newImageAlpha = thisCard.GetComponentsInChildren<UnityEngine.UI.Image>()[1].color;
        Color transparent = new Color(1, 1, 1, 0);
        Color currentTextColor = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().color;
        Color targetTextColor = new Color(currentTextColor.r, currentTextColor.g, currentTextColor.b, 0);
        while (parameter < 1)
        {
            gameObject.GetComponentsInChildren<UnityEngine.UI.Image>()[0].color = Color.Lerp(transparent, Color.white, parameter);
            gameObject.GetComponentsInChildren<UnityEngine.UI.Image>()[1].color = Color.Lerp(Color.white, transparent, parameter);
            //gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.Lerp(Color.white, transparent, parameter);
            gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.Lerp(currentTextColor, targetTextColor, parameter);
            //gameObject.transform.localScale.Set(Mathf.Lerp(1, sizeWhenHovering, parameter), Mathf.Lerp(sizeWhenHovering, 1, parameter), 1);

            parameter += 0.01f;
            yield return null;
        }
        yield return null;
    }

    public IEnumerator SendToDiscard()
    {
        if (gameObject.activeInHierarchy)
        {
            Transform initialPosition = gameObject.transform;
            var parameter = 0.0f;
            while (parameter < 1)
            {
                gameObject.transform.SetPositionAndRotation(Vector3.Lerp(initialPosition.position, Encounter.discardPileTransform.position, parameter), Quaternion.identity);
                parameter += 0.001f;
                yield return null;
            }
            yield return null;


        }
        
        yield return null;
    }

    public void RemoveFromDiscard()
    {

    }
}
