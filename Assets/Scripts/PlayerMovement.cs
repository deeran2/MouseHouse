using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public GameManager manager;
	public bool usesManager = true;
	public float moveSpeed;
	public Rigidbody rb;
	public GameObject deathParticles;

	private Vector3 input;
	private float maxSpeed = 5f;
	public float turnSpeed = 50f;
	private Vector3 spawn;
	private float vert;

	public AudioSource playerAudio;
	public AudioClip[] audioClip;
	private Animator anim;

	void Start () {
		anim = GetComponentInChildren<Animator> ();
		playerAudio = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
		spawn = transform.position;
		if (usesManager) {
 			manager = manager.GetComponent<GameManager>();
		}

	}
	
	void FixedUpdate () {
		if (rb.velocity.magnitude < maxSpeed) {
			rb.AddForce (input * moveSpeed);

		}

		//input = new Vector3 (0f, 0f, Input.GetAxisRaw("Vertical"));
		if(Input.GetKey(KeyCode.UpArrow))
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		if(Input.GetKey(KeyCode.DownArrow))
			transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
		if(Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
		if(Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) 
			|| Input.GetKey(KeyCode.RightArrow)) {
			anim.SetBool ("walk", true);
		} else {
			anim.SetBool ("walk", false);
		}

		if (transform.position.y <-2) {
			Die ();
		}
	}


	void OnCollisionEnter(Collision other){

		if (other.transform.tag == "Enemy") {
			Die ();
		}
	}

	void OnTriggerEnter(Collider other){

		if (other.transform.tag == "Enemy") {
			Die ();
		}

		if (other.transform.tag == "Goal") {
			playerAudio.clip = audioClip[1];
			playerAudio.Play ();
			Time.timeScale = 0f;
			manager.CompleteLevel ();
		}
		if (other.transform.tag == "Token") {
			if (usesManager) {
				manager.tokenCount += 1;
			}
			playerAudio.clip = audioClip[0];
			playerAudio.Play ();
			Destroy (other.gameObject);
		}

		
	}

	void Die(){
		Instantiate (deathParticles, transform.position, Quaternion.identity);
		transform.position = spawn;
		}
	}
