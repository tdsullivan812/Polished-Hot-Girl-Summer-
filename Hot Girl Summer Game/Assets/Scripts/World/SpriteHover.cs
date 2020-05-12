using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHover : MonoBehaviour
{
    SpriteRenderer renderer;

    void Start()
    {
        renderer = this.GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter()
    {
        renderer.color = new Color(242f, 34f, 169f, .8f);
    }

    void OnMouseExit()
    {
        renderer.color = new Color(255f, 255f, 255f, 1f);
    }
}
