using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	//Count
	public  int currentScore;
	public  int highScore;
	public int tokenCount;
	private int totalTokenCount;
	public   int currentLevel;
	public  int unlockedLevel;
	//Timers
	public float startTime;
	private float currentTime;
	public float countFrom;

	//GUI
	public GUISkin skin;
	public Rect timerRect;
	public string timerText;
	public int level;
	public Color warningColor;
	public Color defaultColor;

	//References
	public GameObject tokenParent; 
	private bool showWinScrn = false;
	private int winScrnWidth = Screen.width / 4; 
	private int winScrnHeight = Screen.height / 3;



	void Start (){
		startTime = countFrom;
		Time.timeScale = 1f;
		totalTokenCount = tokenParent.transform.childCount;
		/*if(PlayerPrefs.GetInt ("Level Complete") >0){
			currentLevel = PlayerPrefs.GetInt ("Level Complete");
		}
		else{
			currentLevel = 1;}*/

	}

	public  void CompleteLevel(){

		currentLevel += 1;
	
		showWinScrn = true;		

	}
	void LoadNextLevel(){
		
	

		//if (currentLevel < SceneManager.sceneCountInBuildSettings) {
			SceneManager.LoadScene ("Level" + currentLevel);
		

	}

	void SaveGame(){
		if(currentLevel > PlayerPrefs.GetInt("Level Complete")){
		PlayerPrefs.SetInt ("Level Complete", currentLevel);
		//PlayerPrefs.SetInt ("Level " + currentLevel.ToString() + " score", currentScore);
	}
	}

	void Update(){
		/*if (currentLevel == 0) {
			currentLevel += 1;
		}*/
	
		if (showWinScrn == false) {
			float t = startTime - Time.timeSinceLevelLoad;
			string minutes = ((int)t / 60).ToString ();
			string seconds = (t % 60).ToString ("f1");
			timerText = minutes + ":" + seconds;

			if (Time.timeSinceLevelLoad >= countFrom) {
				timerText = "0:0.0";
				Debug.Log ("Game Over");

				SceneManager.LoadScene ("MainMenu");
			}
		}
	}
	void OnGUI(){
				
	
		GUI.skin = skin;
		if (Time.time > countFrom - 5) {
			skin.GetStyle ("Timer").normal.textColor = warningColor;}
		else {
			skin.GetStyle ("Timer").normal.textColor = defaultColor;
		}
		GUI.Label (new Rect(45,100,200,200), tokenCount.ToString() + "/" + totalTokenCount.ToString());  
		GUI.Label (timerRect, timerText, skin.GetStyle("Timer")); 
		if (showWinScrn) {
			SaveGame ();

			Scene scene = SceneManager.GetActiveScene ();

			Rect winScrnRect = new Rect (Screen.width / 2 - (winScrnWidth/2), Screen.height / 2 - (winScrnHeight/2), winScrnWidth, winScrnHeight);
			GUI.Box (winScrnRect, scene.name +  " Complete!");


			if(currentLevel != SceneManager.sceneCountInBuildSettings - 1){
				if (GUI.Button (new Rect (winScrnRect.x + (winScrnWidth / 2 - 35), winScrnRect.y + (winScrnHeight / 2 - 15), 70, 25), "Continue")) {
				LoadNextLevel ();
			}
			}else {
				GUI.Label (new Rect (winScrnRect.x + (winScrnWidth / 2 - 35), winScrnRect.y + (winScrnHeight / 2 - 15), 70, 25), "You Win!", skin.GetStyle("Winner"));
			}

			if (GUI.Button (new Rect (winScrnRect.x + (winScrnWidth / 2 - 35), winScrnRect.y + (winScrnHeight / 2 + 20), 70, 25), "Quit")) {
				SceneManager.LoadScene ("MainMenu");
				Time.timeScale = 1f;

			}
		}
	}

}
