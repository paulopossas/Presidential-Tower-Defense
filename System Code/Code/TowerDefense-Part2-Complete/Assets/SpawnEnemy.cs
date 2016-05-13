using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave {
	public GameObject [] enemyPrefabs;
	public float spawnInterval = 2;
	public int maxEnemies;
	public List<GameObject> prefabs;

}

public class SpawnEnemy : MonoBehaviour {

	public GameObject[] waypoints;
	public Wave[] waves;
	public int timeBetweenWaves = 5;
	public int enemyCount = 0;
	
	private GameManagerBehavior gameManager;
	
	private float lastSpawnTime;
	private int enemiesSpawned = 0;

	public GameObject[] validEnemies;


	// Use this for initialization
	void Start () {
		lastSpawnTime = Time.time;
		gameManager =
			GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

		for (int i = 0; i < waves.Length; ++i) {

			if (waves [i].enemyPrefabs.Length < 1) {
				GenerateRandomWave(waves[i], 5, 20);
			}
		}
		
	}


	void GenerateRandomWave(Wave w, int minLength, int maxLength) {
		int randomNumber = (int) (Random.value * (maxLength - minLength)) + minLength;

		GenerateWave (randomNumber, w);
	}

	void GenerateWave(int numEnemies, Wave w) {
		if (validEnemies.Length > 0) {


			//w.enemyPrefabs.Initialize ();
			GameObject[] enemies = new GameObject[numEnemies];
			w.enemyPrefabs = enemies;
			//enemies.CopyTo (w.enemyPrefabs, 0);

			int playerFaction = PlayerPrefs.GetInt ("Faction");
			w.maxEnemies = numEnemies;
			bool decided = false;
			//Random.seed (65535);
			for (int i = 0; i < numEnemies; ++i) {
				while (!decided) {
					int randomNumber = ((int)(Random.value * 1000f)) % validEnemies.Length;
					int candidateEnemyFaction = validEnemies [randomNumber].GetComponent<MoveEnemy> ().faction;
					//Debug.Log ("player faction :"+playerFaction);
					//Debug.Log ("enemy faction :" +candidateEnemyFaction);
					if (candidateEnemyFaction != playerFaction) {
						w.enemyPrefabs[i] = validEnemies[randomNumber];
						//Debug.Log ("enemy added");
						decided = true;
					}
				}
				decided = false;
			}
			//enemies.CopyTo (w.enemyPrefabs, 0);

			randomizeSpawnInterval (w, 0.5f, 4f);

		} else {
		}
	}

	void randomizeSpawnInterval(Wave w, float minInterval, float maxInterval)
	{
		w.spawnInterval = Random.value * (maxInterval - minInterval) + minInterval;
	}
	
	// Update is called once per frame
	void Update () {
		// 1
		int currentWave = gameManager.Wave;
		if (currentWave < waves.Length) {
			// 2
			float timeInterval = Time.time - lastSpawnTime;
			float spawnInterval = waves[currentWave].spawnInterval;
			if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
				timeInterval > spawnInterval) && 
				enemiesSpawned < waves[currentWave].maxEnemies) {
				// 3  
				lastSpawnTime = Time.time;
				GameObject newEnemy = (GameObject)Instantiate(waves[currentWave].enemyPrefabs[enemyCount]);
				//Debug.Log (currentWave);
				//Debug.Log (enemyCount);
				if (enemyCount < waves [currentWave].enemyPrefabs.Length) {
					enemyCount++;
				}
				newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
				enemiesSpawned++;
			}

			// 4 
			if (enemiesSpawned == waves[currentWave].maxEnemies &&
			    GameObject.FindGameObjectWithTag("Enemy") == null) {

				gameManager.Wave++;
				enemyCount = 0;
				gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
				enemiesSpawned = 0;
				lastSpawnTime = Time.time;
			}
			// 5 
		} else {
			int playLevel = PlayerPrefs.GetInt ("PlayLevel");
			Debug.Log (playLevel);
			switch (playLevel) {
			case 1:
				UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/MapOneEnd");
				break;
			case 2:
				UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/MapThreeEnd");
				break;
			case 3:
				UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/MapThreeEnd");
				break;
			}
			/*
			gameManager.gameOver = true;
			GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");
			gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
			*/
		}	
	}
}
