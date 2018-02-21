using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrap : MonoBehaviour {
	private Animator anim;
	void Start () {
		anim = GetComponent<Animator> ();
	}



	//When stepping on the platform, the trap will trigger and play audio

	void OnTriggerStay(Collider other){
		if(other.transform.tag == "Player"){
			anim.SetTrigger ("Swing");


		}
	}
}
