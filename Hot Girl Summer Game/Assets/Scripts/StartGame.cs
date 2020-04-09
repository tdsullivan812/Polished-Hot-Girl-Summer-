using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void clickToPlay()
    {
        //SceneManager.LoadScene(1);
        Services.gameController.LoadQuiz();

    }

    public void testPlay()
    {
        //SceneManager.LoadScene(2);
        Services.gameController.LoadParty();
    }
}
