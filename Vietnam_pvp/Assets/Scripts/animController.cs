using UnityEngine;
using System.Collections;

public class animController : MonoBehaviour {

    Animator animStateSetter;
    movement stateSender;
    shootingScript shooting;

	// Use this for initialization
	void Start () {
        animStateSetter = GetComponent<Animator>();
        stateSender = GetComponent<movement>();
        shooting = GetComponent<shootingScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (stateSender.moving == false) ChangeAnimationState(0, ("animState")); //animacija za idle
        if (stateSender.moving == true) ChangeAnimationState(1, ("animState")); // animacija za walking
        if (stateSender.ducking && !stateSender.moving) ChangeAnimationState(2, ("animState")); //animacija za ducking
        else if (stateSender.ducking && stateSender.moving) ChangeAnimationState(6, "animState"); //animacija za duck and movea

        if (shooting.shoot)ChangeAnimationState(1, ("shootState")); //animacija za streljanje
        if (!shooting.shoot) ChangeAnimationState(0, ("shootState")); //animacija za ne streljanje
       
        if (stateSender.grounded == false) ChangeAnimationState(4,( "animState")); // animacija za jump
        if (stateSender.action && stateSender.grounded && !stateSender.moving) ChangeAnimationState(5, ("animState")); //animacija za Actions  
        else if(stateSender.action && stateSender.grounded && stateSender.moving) ChangeAnimationState(7, ("animState"));

    }

    void ChangeAnimationState(int value,string name)
    {
        animStateSetter.SetInteger(name, value);
    }
}
