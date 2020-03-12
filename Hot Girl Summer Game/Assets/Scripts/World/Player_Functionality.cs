using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Functionality : MonoBehaviour
{
    public int moveSpeed; //speed of character

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //player movement
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))//move right
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //move left
        {
            transform.position += Vector3.right * -moveSpeed * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //move back
        {
            transform.position += Vector3.back * -moveSpeed * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) //move forward
        {
            transform.position += Vector3.forward * -moveSpeed * Time.deltaTime;

        }



    }

}
