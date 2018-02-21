using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Epilogue : MonoBehaviour {

	public GameObject page1;
	public GameObject page2;
	public GameObject page3;

	public void NextPage1(){
		page1.SetActive (false);
		page2.SetActive (true);

	}

	public void NextPage2(){

		page2.SetActive (false);
		page3.SetActive (true);
	}

	public void EndGame(){
		SceneManager.LoadScene ("MainMenu");
	}
}
