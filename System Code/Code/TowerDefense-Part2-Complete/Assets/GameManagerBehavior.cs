using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour {

	public bool isPaused = false;
	public bool hasMusic;
	public bool hasSound;

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


	public void establishPauseMenu(GameObject p)
	{
		PauseMenu = p;
		print ("Established Pause Menu");
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

	// Use this for initialization
	void Start () {
		Gold = 1000;
		Wave = 0;
		Health = 5;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
