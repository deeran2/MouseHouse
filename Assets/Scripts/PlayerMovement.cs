using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public GameManager manager;
	public bool usesManager = true;
	public float moveSpeed;
	private bool canMove = true;
	public Rigidbody rb;
	public GameObject deathParticles;


	public float turnSpeed = 50f;
	private Vector3 spawn;
	private Quaternion spawnrot;
	private float vert;

	public AudioSource playerAudio;
	public AudioClip[] audioClip;
	private Animator anim;

	void Start () {
		anim = GetComponentInChildren<Animator> ();
		playerAudio = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
		spawn = transform.position;
		spawnrot = transform.rotation;
		if (usesManager) {
 			manager = manager.GetComponent<GameManager>();
		}

	}
	
	void FixedUpdate () {

		//Movement
		if (canMove) {
			if (Input.GetKey (KeyCode.UpArrow))
				transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
			if (Input.GetKey (KeyCode.DownArrow))
				transform.Translate (-Vector3.forward * moveSpeed * Time.deltaTime);
			if (Input.GetKey (KeyCode.LeftArrow))
				transform.Rotate (Vector3.up, -turnSpeed * Time.deltaTime);
			if (Input.GetKey (KeyCode.RightArrow))
				transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
		

			//Animation for walking

			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.LeftArrow)
			   || Input.GetKey (KeyCode.RightArrow)) {
				anim.SetBool ("walk", true);
			} else {
				anim.SetBool ("walk", false);
			}
		}

		//Falling of the edge kills player

		if (transform.position.y <-2) {
			playerAudio.clip = audioClip[2];
			playerAudio.Play ();
			Die ();

		}
	}


	void OnCollisionEnter(Collision other){

		//Colliding with enemy kills player

		if (other.transform.tag == "Enemy") {
			StartCoroutine (Dead ());
			playerAudio.clip = audioClip[6];
			playerAudio.Play ();
		}
	}

	//Colliding with enemy kills player and plays die clip

	void OnTriggerEnter(Collider other){


		//Mouse trap kills player

		if (other.transform.tag == "mouseTrap") {
			StartCoroutine (Dead ());
			playerAudio.clip = audioClip[3];
			playerAudio.Play ();
		}

		//Reaching goal will play clip, stop time, play winscrn, and add to current level

		if (other.transform.tag == "Goal") {
			playerAudio.clip = audioClip[1];
			playerAudio.Play ();
			Time.timeScale = 0f;
			manager.CompleteLevel ();
		}

		//Collecting a token adds to the token counter, plays eating clip, and destroys token

		if (other.transform.tag == "Token") {
			if (usesManager) {
				manager.tokenCount += 1;
			}
			playerAudio.clip = audioClip[0];
			playerAudio.Play ();
			Destroy (other.gameObject);
		}

		//Collecting poison kills and destroys poison

		if (other.transform.tag == "Poison") {

			StartCoroutine (Dead ());
			playerAudio.clip = audioClip[2];
			playerAudio.Play ();
			Destroy (other.gameObject);
		}

		//Walking into gas collider kills and plays clip
		if (other.transform.tag == "Gas") {

			playerAudio.clip = audioClip[4];
			playerAudio.Play ();
			StartCoroutine (Dead ());
		}

		//Walking into spike collider kills and plays clip
		if (other.transform.tag == "spikes") {

			playerAudio.clip = audioClip[5];
			playerAudio.Play ();
			StartCoroutine (Dead ());
		}
		
	}
	//Plays death particles and moves player back to spawn point

	void Die(){
		Instantiate (deathParticles, transform.position, Quaternion.identity);
		transform.position = spawn;
		transform.rotation = spawnrot;
		manager.died = true;
		}

	//Disables walking while respawning player
	IEnumerator Dead(){

		canMove = false;
		anim.SetBool ("walk", false);
		anim.SetTrigger ("death");	

		yield return new WaitForSeconds(1);
		anim.ResetTrigger ("death");
		transform.position = spawn;
		transform.rotation = spawnrot;
		manager.died = true;
		canMove = true;

		}

	}
