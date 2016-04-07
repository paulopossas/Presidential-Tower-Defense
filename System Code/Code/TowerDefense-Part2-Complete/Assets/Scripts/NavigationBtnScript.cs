using UnityEngine;
using System.Collections;

public class NavigationBtnScript : MonoBehaviour {

	public GameObject mainMenuBtns;
	public GameObject factionSelectBtns;
	public GameObject levelSelectBtns;

	public GameObject helpMenuView;

	public bool sound = true;
	public bool music = true;

	// this is changed as the user makes decisions about their faction.
	public int faction = 0; // 1 for democrat, 2 for republican.


	void Start()
	{
		//helpMenuView = GameObject.FindGameObjectWithTag ("HelpMenu");

	}

	// Use this for initialization
	void Awake () {
		mainMenuBtns = GameObject.FindGameObjectWithTag ("MainMenu");
		factionSelectBtns = GameObject.FindGameObjectWithTag ("SelectFaction");
		levelSelectBtns = GameObject.FindGameObjectWithTag ("SelectLevel");

		mainMenuBtns.transform.localScale = new Vector3(1,1,1);
		factionSelectBtns.transform.localScale = new Vector3(0,0,0);
		levelSelectBtns.transform.localScale = new Vector3(0,0,0);

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
		print ("Move to chosen level");
		UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/GameScene");
		//Application.Load ("../GameScene.unity");
	}

	public void ToMainMenu()
	{
		mainMenuBtns.transform.localScale = new Vector3(1,1,1);
		factionSelectBtns.transform.localScale = new Vector3(0,0,0);
		levelSelectBtns.transform.localScale = new Vector3(0,0,0);

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
		sound = !sound;
	}

	public void toggleMusic()
	{
		music = !music;
	}

	public void toggleHelp()
	{
		//what should this menu have?
		// TODO: Make the menu actually display properly
	}

	// Update is called once per frame
	void Update () {
	
	}
}
