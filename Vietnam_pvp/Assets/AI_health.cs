using System.Collections;
using UnityEngine;

public class AI_health : MonoBehaviour {

    public int MaxHP;
    public int CurrentHP;
	

	// Update is called once per frame
	void Update () {
	if(CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

    if(CurrentHP <= 0)
        {
            //Play animation or some shit?
            Debug.Log("Enemy Dead");
        }
	}



    public void splatterBlood()
    {
        Debug.Log("SplatterBlood");
    }
}
