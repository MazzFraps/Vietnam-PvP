using UnityEngine;
using System.Collections;

public class skyRotate : MonoBehaviour {
    public float rotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(gameObject.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
