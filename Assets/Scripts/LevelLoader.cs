using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

	public GameManager manager;
	public GameObject padLock;
	public int levelToLoad;
	private string loadPrompt;
	public int completedLevel;
	private bool canLoad = false; 

	void Start(){
		/*if (manager.currentLevel == 0) {
			manager.currentLevel++;
			levelToLoad++;
		}*/
		completedLevel = PlayerPrefs.GetInt ("Level Complete");
		if (levelToLoad <= completedLevel) {
			canLoad = true;		
		}else {Instantiate(padLock, new Vector3(transform.position.x, transform.position.y +2.5f, transform.position.z), Quaternion.identity);

		}

	}

		void OnTriggerStay(Collider other){
		if (canLoad) {
			if (Input.GetButtonDown ("Submit")) {

				
				SceneManager.LoadScene ("Level" + levelToLoad.ToString ());
			}
			loadPrompt = "Enter level " + levelToLoad.ToString ();
		} else {
			loadPrompt = "Unlock level " + levelToLoad.ToString ();
		}
	}
	

	void OnTriggerExit(){
		loadPrompt = "";
	}

	void OnGUI(){

		GUI.Label(new Rect(Screen.width/2 -20, Screen.height/2, 200, 40), loadPrompt);
	}
}
