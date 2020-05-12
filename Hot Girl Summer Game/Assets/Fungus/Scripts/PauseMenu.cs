using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gamePaused == false)
            {
                gamePaused = true;
                pauseMenu.SetActive(true);

            }
            else
            {
                gamePaused = false;
                pauseMenu.SetActive(false);
            }

        }

    }

    public void resumeGame()
    {
        gamePaused = false;
        pauseMenu.SetActive(false);
    }
}
