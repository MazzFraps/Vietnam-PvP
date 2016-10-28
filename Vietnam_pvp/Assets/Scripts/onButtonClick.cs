using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class onButtonClick : MonoBehaviour {

    public void changeScene(string name)
    {
        SceneManager.LoadScene(name);        
    }

    public void exitGame()
    {
        Debug.Log("Exiting the game");
        Application.Quit();
    }
}
