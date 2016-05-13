using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour {

	public bool isPaused = false;
	public bool hasMusic;
	public bool hasSound;
	public int level;
	public static int mapID;

	public bool isRepublican;

	private GameObject PauseMenu;
	private int gameSpeed = 1;

	public Text goldLabel;
	private int gold;
	public int Gold {
  		get { return gold; }
  		set {
			gold = value;
    		goldLabel.GetComponent<Text>().text = "GOLD: " + gold;
		}
	}

	// Use this for initialization
	void Start () {
		
		Gold = 1000;
		Wave = 0;
		Health = 5;

		setSpeed (1);

		int party = PlayerPrefs.GetInt ("faction");

		if (party == 1) {
			isRepublican = false;
		} else if (party == 2) {
			isRepublican = true;
		} else {
			isRepublican = true;
		}


		// checking if it works
		if (isRepublican) {
			//print ("Republican party chosen");
		} else {
			//print ("Democrat party chosen");
		}

		level = PlayerPrefs.GetInt ("level");
		//TO DO: do something with the level

		int music = PlayerPrefs.GetInt ("music");
		int sound = PlayerPrefs.GetInt ("sound");

		//print ("has sound: " + sound);
		//print ("has music: " + music);

		hasMusic = (music == 1);
		hasSound = (sound == 1);

		if (hasMusic) {
			onmusic ();
		} else {
			offmusic ();
		}

		if (hasSound) {
			unmute ();
		} else {
			mute ();
			offmusic (); // this line might be unneeded
		}
	}

	// modifies the road and background (and possibly the spawner) to match the given level
	void loadLevel() {

	}


	public void establishPauseMenu(GameObject p)
	{
		PauseMenu = p;
		//print ("Established Pause Menu");
	}

	public void setSpeed(int x)
	{
		if (x > 0) {
			if (x < 20) {
				
				gameSpeed = x;

				if (!isPaused) {
					Time.timeScale = x;
				}
			}
		}
	}

	public void pause()
	{
		

		Time.timeScale = 0;
		/*
		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
		
		for (int i = 0; i < enemys.Length; ++i) {
			enemys [i].GetComponent<MoveEnemy> ().pause ();
		}

		GameObject[] towers = GameObject.FindGameObjectsWithTag("Monster");

		for (int i = 0; i < towers.Length; ++i) {
			towers [i].GetComponent<ShootEnemies> ().pause ();
		}

		

		GameObject.Find ("Road").GetComponent<SpawnEnemy> ().pause ();

		*/

		PauseMenu.GetComponentInChildren<PauseMenuScript> ().openMenu ();

		isPaused = true;
	}

	public void unpause()
	{
		/*
		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

		for (int i = 0; i < enemys.Length; ++i) {
			enemys [i].GetComponent<MoveEnemy> ().unpause ();
		}

		GameObject[] towers = GameObject.FindGameObjectsWithTag("Monster");
		for (int i = 0; i < towers.Length; ++i) {
			towers [i].GetComponent<ShootEnemies> ().unpause ();
		}

		GameObject.Find ("Road").GetComponent<SpawnEnemy> ().unpause ();
		*/
		Time.timeScale = gameSpeed;

		PauseMenu.GetComponentInChildren<PauseMenuScript> ().hideMenu ();

		isPaused = false;
	}

	public void mute()
	{
		AudioListener.pause = true;
		hasSound = false;
	}

	public void unmute()
	{
		AudioListener.pause = false;
		hasSound = true;
	}

	public void offmusic()
	{
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource> ().volume = 0;
		hasMusic = false;
	}

	public void onmusic()
	{
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource> ().volume = 0.1f;
		hasMusic = true;
	}

	public Text waveLabel;
	public GameObject[] nextWaveLabels;

	public bool gameOver = false;

	private int wave;
	public int Wave {
		get { return wave; }
		set {
			wave = value;
			if (!gameOver) {
				for (int i = 0; i < nextWaveLabels.Length; i++) {
					nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
				}
			}
			waveLabel.text = "WAVE: " + (wave + 1);
		}
	}

	public Text healthLabel;
	public GameObject[] healthIndicator;

	private int health;
	public int Health {
		get { return health; }
		set {
			// 1
			if (value < health) {
				Camera.main.GetComponent<CameraShake>().Shake();
			}
			// 2
			health = value;
			healthLabel.text = "HEALTH: " + health;
			// 2
			if (health <= 0 && !gameOver) {
				gameOver = true;
				UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/FailedMap");
				GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameOver");
				gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
			}
			// 3 
			for (int i = 0; i < healthIndicator.Length; i++) {
				if (i < Health) {
					healthIndicator[i].SetActive(true);
				} else {
					healthIndicator[i].SetActive(false);
				}
			}
		}
	}




	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space)) {
			if (isPaused) {
				unpause ();
			} else {
				pause ();
			}
		}

		if (Input.GetKeyUp (KeyCode.Alpha5)) {
			setSpeed (5);
		}
		if (Input.GetKeyUp (KeyCode.Alpha3)) {
			setSpeed (3);
		}
		if (Input.GetKeyUp (KeyCode.Alpha2)) {
			setSpeed (2);
		}
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			setSpeed (1);
		}
		if (Input.GetKeyUp (KeyCode.Alpha0)) {
			setSpeed (10);
		}
	}
}
