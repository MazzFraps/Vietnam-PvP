using UnityEngine;
using System.Collections;

public class dayNight : MonoBehaviour {

    Light lt;
    public bool day = true;
   float time = 0.00001f;

	// Use this for initialization
	void Start () {
        lt = GetComponent<Light>();
        
	}

    // Update is called once per frame
    void Update () {
        changeTime();
        if (lt.intensity > 1.4)
        {
            day = true;
        }
        else {

            day = false;

        }
	}

    public void changeTime()
    {
        lt.intensity += time;
        
        if (lt.intensity >= 2.2 || lt.intensity == 0)
        {
            time = time * -1;
            
        }
    }
}
