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

	//Timers
	public float startTime;
	private float currentTime;
	public float countFrom;

	//Stars
	public bool died = false;
	public bool starTime = true;
	private int tokensCollected;
	public int starsearned = 0;

	//GUI
	public GUISkin skin;
	public Rect timerRect;
	public string timerText;
	public Color warningColor;
	public Color defaultColor;

	//References
	public GameObject tokenParent; 
	private bool showWinScrn = false;
	public bool showLoseScrn = false;


	private int winScrnWidth = Screen.width / 4; 
	private int winScrnHeight = Screen.height / 3;



	void Start (){

		startTime = countFrom;
		Time.timeScale = 1f; 
		totalTokenCount = tokenParent.transform.childCount; //Total available tokens from number of children in Tokens gameobject
	

	}

	public  void CompleteLevel(){

		//Player is given a star for not dying, collecting all tokens, completing level withing 1/2 of time given
		//Stars are saved per level, current level is inscreased and win screen is enabled

		if (died == false) {starsearned += 1;}
		if (totalTokenCount == tokenCount) {starsearned += 1;}
		if (starTime == true) {starsearned += 1;}
		if (starsearned > PlayerPrefs.GetInt ("Level" + currentLevel.ToString() + "stars"))
		{
			PlayerPrefs.SetInt ("Level" + currentLevel.ToString() + "stars", starsearned);
		}

		currentLevel += 1;
		showWinScrn = true;		

	}

	//Saves highest current level of player

	void SaveGame(){
		if (currentLevel > PlayerPrefs.GetInt ("Level Complete")) {
			PlayerPrefs.SetInt ("Level Complete", currentLevel);
		}

	}

	void Update(){

		//While Wins screen is false, play timer

		if (showWinScrn == false) {
			float t = startTime - Time.timeSinceLevelLoad;
			string minutes = ((int)t / 60).ToString ();
			string seconds = (t % 60).ToString ("f1");
			timerText = minutes + ":" + seconds;

			if (Time.timeSinceLevelLoad >= countFrom/2) {
				starTime = false;
			}

		//If timer runs out, play lose screen

			if (Time.timeSinceLevelLoad >= countFrom) {
				timerText = "0:0.0";
				Debug.Log ("Game Over");
				showLoseScrn = true;
			}
		}
	}
	void OnGUI(){
				
		//In last 5 seconds, timer color switches to red

		GUI.skin = skin;
		if (Time.timeSinceLevelLoad > countFrom - 5) {
			skin.GetStyle ("Timer").normal.textColor = warningColor;}
		else {
			skin.GetStyle ("Timer").normal.textColor = defaultColor;
		}

		//GUI for token count and timer

		GUI.Label (new Rect(45,100,200,200), tokenCount.ToString() + "/" + totalTokenCount.ToString());  
		GUI.Label (timerRect, timerText, skin.GetStyle("Timer")); 

		//Win screen GUI

		if (showWinScrn) {
			
			SaveGame ();

			Scene scene = SceneManager.GetActiveScene ();

			//Final level has different GUI


			if(currentLevel == SceneManager.sceneCountInBuildSettings - 2){

				Rect winScrnRect = new Rect (Screen.width / 2 - (winScrnWidth/2), Screen.height / 2 - (winScrnHeight/2), winScrnWidth, winScrnHeight);
				GUI.Box (winScrnRect, " ");


				GUI.Label (new Rect (winScrnRect.x + (winScrnWidth / 2 - 55), winScrnRect.y + (winScrnHeight / 2 - 15), 70, 25), "You Escaped!", skin.GetStyle("Winner"));

				if (GUI.Button (new Rect (winScrnRect.x + (winScrnWidth / 2 - 35), winScrnRect.y + (winScrnHeight / 2 + 20), 70, 25), "Next")) {
					SceneManager.LoadScene ("Epilogue");
				}

			} else{

				Rect winScrnRect = new Rect (Screen.width / 2 - (winScrnWidth/2), Screen.height / 2 - (winScrnHeight/2), winScrnWidth, winScrnHeight);
				GUI.Box (winScrnRect, scene.name +  " Complete!");

				if (GUI.Button (new Rect (winScrnRect.x + (winScrnWidth / 2 - 35), winScrnRect.y + (winScrnHeight / 2 - 15), 70, 25), "Continue")) {
					SceneManager.LoadScene ("Level" + currentLevel);
				}

					if (GUI.Button (new Rect (winScrnRect.x + (winScrnWidth / 2 - 35), winScrnRect.y + (winScrnHeight / 2 + 20), 70, 25), "Quit")) {
						SceneManager.LoadScene ("MainMenu");
						Time.timeScale = 1f;

					}
			
			}



		}
	
		//Lose screen GUI

		if(showLoseScrn){

		Rect winScrnRect = new Rect (Screen.width / 2 - (winScrnWidth/2), Screen.height / 2 - (winScrnHeight/2), winScrnWidth, winScrnHeight);
		GUI.Box (winScrnRect, "Time's Up!");



		if (GUI.Button (new Rect (winScrnRect.x + (winScrnWidth / 2 - 35), winScrnRect.y + (winScrnHeight / 2 - 15), 70, 25), "Retry?")) {
			SceneManager.LoadScene ("Level" + currentLevel);
		}

		if (GUI.Button (new Rect (winScrnRect.x + (winScrnWidth / 2 - 35), winScrnRect.y + (winScrnHeight / 2 + 20), 70, 25), "Quit")) {
			SceneManager.LoadScene ("MainMenu");
		}
	}
	}
}
