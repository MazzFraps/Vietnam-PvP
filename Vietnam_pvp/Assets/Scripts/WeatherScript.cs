using UnityEngine;
using System.Collections;

public class WeatherScript : MonoBehaviour {

    ParticleSystem rain;
    Transform targetX;

	// Use this for initialization
	void Start () {
        rain = GetComponent<ParticleSystem>();
        //targetX = FindObjectOfType<camera>();
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.position = new Vector3(playerX.transform.position.x, transform.position.y, transform.position.z);
	}
}
