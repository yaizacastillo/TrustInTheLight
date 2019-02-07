using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorScript : MonoBehaviour {

    private Animator myAnimator;
    private bool doorOpened;

	void Start () {
        myAnimator = GetComponent<Animator>();
	}
	
    public void OpenDoor()
    {
        if (doorOpened)
            return;

        myAnimator.SetTrigger("Open");
        doorOpened = true;
    }

}
