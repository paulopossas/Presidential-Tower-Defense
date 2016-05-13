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

[System.Serializable]
public class WaveRandomizerDifficulty {
	public float baseMinInterval = 1f;
	public float baseMaxInterval = 4f;

	public float endMinInterval = 1f;
	public float endMaxInterval = 4f;

	public int baseMinEnemies = 5;
	public int baseMaxEnemies = 20;

	public int endMinEnemies = 5;
	public int endMaxEnemies = 20;

	public bool linearScaling = true;
}

public class SpawnEnemy : MonoBehaviour {

	public GameObject[] waypoints;

	public WaveRandomizerDifficulty waveRandomizerDifficulty;

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

				float difficultyFactor = 0;
				if (waveRandomizerDifficulty.linearScaling) {
					difficultyFactor = ((float)(i)) / waves.Length;
				} else {
					// should be quadratic scaling (I think)
					difficultyFactor = ((float)(i)) / waves.Length * ((float)(i)) / waves.Length;
				}
				float baseMinEnemyRange = (float)(waveRandomizerDifficulty.endMinEnemies - waveRandomizerDifficulty.baseMinEnemies);

				int minEnemies = (int) (baseMinEnemyRange * difficultyFactor + waveRandomizerDifficulty.baseMinEnemies);

				float baseMaxEnemyRange = (float)(waveRandomizerDifficulty.endMaxEnemies - waveRandomizerDifficulty.baseMaxEnemies);
				int maxEnemies = (int) (baseMaxEnemyRange * difficultyFactor + waveRandomizerDifficulty.baseMaxEnemies);

				float baseMinIntervalRange = waveRandomizerDifficulty.endMinInterval - waveRandomizerDifficulty.baseMinInterval;
				float minSpawnInterval = baseMinIntervalRange * difficultyFactor + waveRandomizerDifficulty.baseMinInterval;

				float baseMaxIntervalRange = waveRandomizerDifficulty.endMaxInterval - waveRandomizerDifficulty.baseMaxInterval;
				float maxSpawnInterval = baseMaxIntervalRange * difficultyFactor + waveRandomizerDifficulty.baseMaxInterval;

				GenerateRandomWave(waves[i], minEnemies, maxEnemies, minSpawnInterval, maxSpawnInterval);
			}
		}
		
	}


	void GenerateRandomWave(Wave w, int minLength, int maxLength, float minInterval, float maxInterval) {
		int randomNumber = (int) (Random.value * (maxLength - minLength)) + minLength;
		float randomNumber2 = Random.value * (maxInterval - minInterval) + minInterval;
		GenerateWave (randomNumber, randomNumber2, w);
	}

	void GenerateWave(int numEnemies, float spawnInterval, Wave w) {
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

			w.spawnInterval = spawnInterval;
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
				print ("next wave: " + gameManager.Wave);
				enemyCount = 0;
				gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
				enemiesSpawned = 0;
				lastSpawnTime = Time.time;
			}
			// 5 
		} else {

			if (!gameManager.gameOver) {
				
				int playLevel = PlayerPrefs.GetInt ("MaxLevel");
				if (playLevel < PlayerPrefs.GetInt ("CurrentLevel")) {
					playLevel = PlayerPrefs.GetInt ("CurrentLevel"); // fixes a bug when playing in the editor
				}
				print ("maxLevel: " + playLevel);
				print ("beaten level: " + PlayerPrefs.GetInt ("CurrentLevel"));
				UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/LevelEnd");
				//Debug.Log (playLevel);

				/*
				switch (playLevel) {
				case 1:
					UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/MapOneEnd");
					break;
				case 2:
					UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/MapTwoEnd");
					break;
				case 3:
					UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/MapThreeEnd");
					break;
				case 0:
					UnityEngine.SceneManagement.SceneManager.LoadScene ("scenes/TitleScreen");
					break;
				}
				*/
				/*
				gameManager.gameOver = true;
				GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");
				gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
				*/
			}
		}	
	}
}
