using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

	private GameManagerBehavior gameManager;
	private Transform musicBtn;
	private Transform soundBtn;
	//private Transform resumeBtn;
	//private Transform mainBtn;
	//private Transform quitBtn;



	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
		gameManager.establishPauseMenu (gameObject);

		musicBtn = transform.parent.transform.FindChild("Music");
		soundBtn = transform.parent.transform.FindChild("Sound");
		//resumeBtn = transform.parent.transform.FindChild("Resume");
		//mainBtn = transform.parent.transform.FindChild("Main");
		//quitBtn = transform.parent.transform.FindChild("Quit");


		transform.parent.transform.localScale = new Vector3 (0, 0, 0);
	}

	public void openMenu()
	{
		transform.parent.transform.localScale = new Vector3 (50,50,1);

		if (gameManager.hasSound) {
			// if the game has sound enabled, change the sound button.
			soundBtn.transform.FindChild("X").localScale = new Vector3(0,0,0);
			//print ("sound");
		}

		if (gameManager.hasMusic) {
			// if the game has music enabled, change the music button.
			musicBtn.transform.FindChild("X").localScale = new Vector3(0,0,0);
			//print ("music");
		}
	}

	public void hideMenu()
	{
		transform.parent.transform.localScale = new Vector3 (0,0,0);
	}

	public void toggleSound()
	{
		
		gameManager.hasSound = !gameManager.hasSound;

		if (gameManager.hasSound) {
			soundBtn.transform.FindChild("X").localScale = new Vector3(0,0,0);
		} else {

			soundBtn.transform.FindChild("X").localScale = new Vector3(1,1,1);
		}

	}

	public void toggleMusic()
	{
		gameManager.hasMusic = !gameManager.hasMusic;

		if (gameManager.hasMusic) {
			musicBtn.transform.FindChild("X").localScale = new Vector3(0,0,0);
		} else {
			
			musicBtn.transform.FindChild("X").localScale = new Vector3(1,1,1);
		}

	}


	public void toMain()
	{
		// show confirm exit to main menu

		// for now, just go to main menu
		forceMain();
	}

	public void forceMain()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/TitleScreen");
	}

	public void quit()
	{
		//display confirm quit menu

		//for now, just quit
		forceQuit();
	}

	public void forceQuit()
	{
		Application.Quit ();
	}

	public void unpause()
	{
		// hide menu
		//transform.parent.transform.localScale = new Vector3(0,0,0);

		gameManager.unpause ();
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
