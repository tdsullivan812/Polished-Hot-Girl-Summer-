using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        CustomizableOptions.currentlyDisplayed.IncrementDisplay(false);
    }
}
