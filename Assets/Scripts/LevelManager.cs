using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

	//Dynamic level button creation

	[System.Serializable]
	public class Level{

		public string levelText;
		public int unlocked;
		public bool isInteractable;

	}

	public GameObject button;
	public Transform spacer;
	public List<Level> LevelList;

	// Use this for initialization
	void Start () {

		FillList ();
	}
	
	void FillList(){

		foreach (var level in LevelList) {

			//instantiate level buttons

			GameObject newButton = Instantiate (button) as GameObject;
			LevelButtonNew buttonScript = newButton.GetComponent<LevelButtonNew>();
			buttonScript.buttonText.text = level.levelText;
			if (PlayerPrefs.GetInt ("Level Complete") >= level.unlocked) {
				level.isInteractable = true;
			}


			//If Level complete is high enough, button will be interactable

			buttonScript.unlock = level.unlocked;
			buttonScript.GetComponent<Button> ().interactable = level.isInteractable;
			buttonScript.GetComponent<Button>().onClick.AddListener(() => 
			SceneManager.LoadScene ("Level" + level.levelText ) );

			//Stars earned on each level will be yellow

			if (PlayerPrefs.GetInt ("Level" + level.levelText + "stars") >= 1){buttonScript.star1.SetActive (true);} 
			if (PlayerPrefs.GetInt ("Level" + level.levelText + "stars") >= 2){buttonScript.star2.SetActive(true);} 
			if (PlayerPrefs.GetInt ("Level" + level.levelText + "stars") >= 3){buttonScript.star3.SetActive(true);} 

			newButton.transform.SetParent (spacer, false);
		}
		SaveAll ();
	}

	//Saves what buttons have been unlocked

	void SaveAll(){

			GameObject[] allbuttons = GameObject.FindGameObjectsWithTag ("LevelButton");
			foreach (GameObject buttons in allbuttons) {

				LevelButtonNew savebutton = buttons.GetComponent <LevelButtonNew> ();
				PlayerPrefs.SetInt ("Level" + savebutton.buttonText.text, savebutton.unlock);

		}
	}

	public void BackButton(){
		SceneManager.LoadScene ("MainMenu");

	}
}
