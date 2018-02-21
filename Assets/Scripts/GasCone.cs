using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCone : MonoBehaviour {

	public GameObject gasParticles;

	//When player walks into collider, shoot gas particles

	void OnTriggerStay(Collider other){
		if(other.transform.tag == "Player"){
			Instantiate (gasParticles, transform.position, Quaternion.identity);


		}
	}
}
