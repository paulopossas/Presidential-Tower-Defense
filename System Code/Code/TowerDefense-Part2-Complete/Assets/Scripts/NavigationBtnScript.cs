using UnityEngine;
using System.Collections;

public class NavigationBtnScript : MonoBehaviour {

	public GameObject mainMenuBtns;
	public GameObject factionSelectBtns;
	public GameObject levelSelectBtns;
	public GameObject optionsMenuBtns;

	public GameObject musicBtn;
	public GameObject soundBtn;

	public GameObject helpMenuView;

	public bool sound = false;
	public bool music = false;

	// this is changed as the user makes decisions about their faction.
	public int faction = 0; // 1 for democrat, 2 for republican.


	void Start()
	{
		//helpMenuView = GameObject.FindGameObjectWithTag ("HelpMenu");
		PlayerPrefs.SetInt("PlayLevel", 0);
	}

	// Use this for initialization
	void Awake () {
		mainMenuBtns = GameObject.FindGameObjectWithTag ("MainMenu");
		factionSelectBtns = GameObject.FindGameObjectWithTag ("SelectFaction");
		levelSelectBtns = GameObject.FindGameObjectWithTag ("SelectLevel");
		optionsMenuBtns = GameObject.FindGameObjectWithTag ("OptionsMenu");
		//soundBtn = optionsMenuBtns.transform.FindChild ("SoundBtn");
		//musicBtn = optionsMenuBtns.transform.FindChild ("MusicBtn");

		mainMenuBtns.transform.localScale = new Vector3(1,1,1);
		factionSelectBtns.transform.localScale = new Vector3(0,0,0);
		levelSelectBtns.transform.localScale = new Vector3(0,0,0);
		optionsMenuBtns.transform.localScale = new Vector3(0,0,0);


	}

	public void ChooseFaction(int input)
	{
		bool isValid = true;
		switch (input) {
		case 1:
			faction = 1;
			break;
		case 2:
			faction = 2;
			break;
		default:
			isValid = false;
			break;		
		}
		PlayerPrefs.SetInt ("Faction", faction);
		if (isValid) {
			ToLevelSelect ();
		}
	}

	public void ChooseLevel(int input)
	{
		int levelChosen = 0;
		bool isValid = true;

		switch (input) {
		case 1:
			levelChosen = input;
			break;
		case 2:
			levelChosen = input;
			break;
		default:
			isValid = false;
			break;
		}

		if (isValid) {
			StartLevel (levelChosen);
		}
	}

	public void StartLevel(int levelChosen)
	{
		//print ("Move to chosen level");

		PlayerPrefs.SetInt ("music", music ? 1 : 0);
		PlayerPrefs.SetInt ("sound", sound ? 1 : 0);

		//UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/GameScene");
		switch (levelChosen) {
		case 1:
			PlayerPrefs.SetInt ("PlayLevel", 1);
			UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/Iowa");
			break;
		case 2:
			PlayerPrefs.SetInt ("PlayLevel", 2);
			UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/GameScene");
			break;
		case 3:
			//Still need one more map
			//For now, going to return to TitleScreen
			PlayerPrefs.SetInt ("PlayLevel", 0);
			UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/TitleScreen");
			break;
		}
		//Application.Load ("../GameScene.unity");
	}

	public void ToMainMenu()
	{
		mainMenuBtns.transform.localScale = new Vector3(1,1,1);
		factionSelectBtns.transform.localScale = new Vector3(0,0,0);
		levelSelectBtns.transform.localScale = new Vector3(0,0,0);
	}

	public void ToOptions()
	{
		mainMenuBtns.transform.Find("OptionsBtn").transform.localScale = new Vector3(0,0,0);
		optionsMenuBtns.transform.localScale = new Vector3 (1, 1, 1);
	}

	public void FromOptions()
	{
		mainMenuBtns.transform.Find("OptionsBtn").transform.localScale = new Vector3(1,1,1);
		optionsMenuBtns.transform.localScale = new Vector3 (0,0,0);
	}

	public void ToSelectFaction()
	{
		//print ("Selecting faction");

		mainMenuBtns.transform.localScale = new Vector3(0,0,0);
		factionSelectBtns.transform.localScale = new Vector3(1,1,1);
		levelSelectBtns.transform.localScale = new Vector3(0,0,0);
	}

	public void ToLevelSelect()
	{
		mainMenuBtns.transform.localScale = new Vector3(0,0,0);
		factionSelectBtns.transform.localScale = new Vector3(0,0,0);
		levelSelectBtns.transform.localScale = new Vector3(1,1,1);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

	public void toggleSound()
	{

		if (sound) {
			sound = false;
			optionsMenuBtns.transform.FindChild("SoundBtn").FindChild ("X").localScale = new Vector3 (0.45f,0.45f,0.45f);

			music = false;
			optionsMenuBtns.transform.FindChild("MusicBtn").FindChild ("X").localScale = new Vector3 (0.45f,0.45f,0.45f);
		} else {
			sound = true;
			optionsMenuBtns.transform.FindChild("SoundBtn").FindChild ("X").localScale = new Vector3 (0, 0, 0);
		}
	}

	public void toggleMusic()
	{
		music = !music;

		if (!music) {
			optionsMenuBtns.transform.FindChild("MusicBtn").FindChild ("X").localScale = new Vector3 (0.45f,0.45f,0.45f);
		} else {
			optionsMenuBtns.transform.FindChild("MusicBtn").FindChild ("X").localScale = new Vector3 (0, 0, 0);
		}
	}

	public void toggleHelp()
	{
		//what should this menu have?
		// TODO: Make the menu actually display properly
	}

	public void nextLevel(){
		int playLevel = PlayerPrefs.GetInt ("PlayLevel");
		playLevel++;
		if (playLevel > 3) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/TitleScreen");
		} else {
			StartLevel (playLevel);
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
