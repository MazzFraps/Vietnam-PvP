using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    bool paused;

    Canvas menu;

	// Use this for initialization
	void Start () {
        menu = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
           
         }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {

        }


        }
}
