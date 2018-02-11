using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	public float delayTime;

	// Use this for initialization
	void Start () {
		StartCoroutine (Go ());
	}
	
	IEnumerator Go(){

		while(true){
			yield return new WaitForSeconds(3f);
			gameObject.GetComponent<Animation> ().Play ();

		}
	}
}
