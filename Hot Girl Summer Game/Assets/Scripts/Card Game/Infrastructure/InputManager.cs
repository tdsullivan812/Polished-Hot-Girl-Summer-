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



    // Start is called before the first frame update
    public static void Initialize()
    {
        
        layerMask = 1 << 8;
        results = new List<RaycastResult>();
        cardGameObjects = new List<GameObject>();

    }

    public static void FindGameObjects()
    {
        raycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        cardGameEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            PointerEventData pointerEvent = new PointerEventData(cardGameEventSystem);
            pointerEvent.position = Input.mousePosition;


            raycaster.Raycast(pointerEvent, results);
            Debug.Log("Raycast sent");
            Debug.Assert(results != null, "assertion failed: no hits on raycast");
            //Debug.Log(results[0].gameObject.name);
            if (results.Count > 0)
            {
                if (cardGameObjects.Contains(results[0].gameObject))
                {

                    Debug.Log("Begin drag");
                    results[0].gameObject.transform.SetPositionAndRotation(pointerEvent.position, Quaternion.identity);
                }
            }
            


        }

        results.Clear();
    }


}
