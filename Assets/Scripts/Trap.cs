using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {


	// Use this for initialization
	void Start () {
		StartCoroutine (Go ());
	}

	//Spikes will pop out every 3 seconds

	IEnumerator Go(){

		while(true){
			yield return new WaitForSeconds(3f);
			gameObject.GetComponent<Animation> ().Play ();

		}
	}
}
