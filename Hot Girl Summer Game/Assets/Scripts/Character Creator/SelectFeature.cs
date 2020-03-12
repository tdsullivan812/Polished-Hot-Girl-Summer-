using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFeature : MonoBehaviour
{
    SpriteRenderer optionRenderer;
    Collider2D optionCollider; 

    // Start is called before the first frame update
    void Start()
    {
        optionRenderer = gameObject.GetComponent<SpriteRenderer>();
        optionCollider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        CustomizableOptions.portraitRenderer.sprite = optionRenderer.sprite;
    }
}
