using UnityEngine;
using System.Collections;

public class particleSpawn : MonoBehaviour {

    public int BarrelHP;

    // Use this for initialization
    void Start()
    {        
    }
	// Update is called once per frame
	void Update () {
	if(BarrelHP <= 0)
        {

            Destroy(gameObject);
        }
	}
}
