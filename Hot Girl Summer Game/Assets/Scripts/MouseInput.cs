using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public float scrollThreshold; // how close to  the edge of screen the mouse should be before camera scrolls
    public Vector3 cameraXMovementIncrement;
    public Vector3 cameraZMovementIncrement;
    public Camera mainCamera;
    private Vector3 screenMidpoint;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        screenMidpoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x <= scrollThreshold && transform.position.x >= -35)
        {
            transform.position -= cameraXMovementIncrement; //scroll left

        }
        else if (Input.mousePosition.x >= Screen.width - scrollThreshold)
        {
            transform.position += cameraXMovementIncrement; //scroll right
        }

        if (Input.mousePosition.y <= scrollThreshold && transform.position.z >= -47)
        {
            transform.position -= cameraZMovementIncrement; //scroll out
        }
        else if (Input.mousePosition.y >= Screen.height - scrollThreshold)
        {
            transform.position += cameraZMovementIncrement; // scroll in
        }

        DisableForeground();
    }

    void DisableForeground()
    {
        RaycastHit hit;
        Physics.Raycast(mainCamera.ScreenPointToRay(screenMidpoint), out hit, Mathf.Infinity, 1<<9);
        hit.collider.gameObject.SetActive(false);
    }
}
