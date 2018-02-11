using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {
	public Transform[] patrolPoints;
	public float moveSpeed;
	private int currentPoint;
	private bool endOfRoute;


	// Use this for initialization
	void Start () {
		transform.position = patrolPoints [0].position;
		currentPoint = 0;
		endOfRoute = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position == patrolPoints [currentPoint].position && endOfRoute == false) {
			currentPoint++;
			if (currentPoint >= patrolPoints.Length) {
				endOfRoute = true;
				currentPoint = patrolPoints.Length -1;
			}
		}
		if (currentPoint <= 0) {
			endOfRoute = false;
			currentPoint = 0;
		}
			if (transform.position == patrolPoints[currentPoint].position && endOfRoute == true) {
				currentPoint--;
		}
		transform.position = Vector3.MoveTowards (transform.position, patrolPoints [currentPoint].position, 
			moveSpeed * Time.deltaTime);
	}
}
