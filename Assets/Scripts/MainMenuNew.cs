using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNew : MonoBehaviour {

	public GameObject continueButton;
	public GameObject intro;

	void Start(){
		//If a stat is save, continue will be available

	if (PlayerPrefs.GetInt ("Level Complete") > 1) {
			continueButton.SetActive (true);
		}
	}

	public void Continue(){
		SceneManager.LoadScene ("LevelSelect");
	}

	//Play intro and reset stats

	public void NewGame(){
		intro.SetActive(true);
		PlayerPrefs.SetInt ("Level Complete", 1);
		PlayerPrefs.SetInt ("Level1stars", 0);
		PlayerPrefs.SetInt ("Level2stars", 0);
		PlayerPrefs.SetInt ("Level3stars", 0);
	}

	//Method for intro button

	public void Next(){

		SceneManager.LoadScene ("LevelSelect");

	}

	//Quit game

	public void Quit(){
		Debug.Log ("Has quit game");
		Application.Quit ();
	}
}
