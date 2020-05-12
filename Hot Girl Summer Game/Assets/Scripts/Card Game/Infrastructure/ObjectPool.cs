using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Stack<GameObject>
{
    public GameObject cardGameObject;
    ICardGameElement elementConnectedTo;
    GameObject currentlyActive;

    public ObjectPool(GameObject newCardGameObject)
    {
        cardGameObject = newCardGameObject;
    }

    public void Update()
    {
        /*
        if(Peek() != currentlyActive)
        {
            currentlyActive.SetActive(false);
            if (Peek() != null)
            {
                currentlyActive = Peek();
                currentlyActive.SetActive(true);
            }
        }*/
    }
}
