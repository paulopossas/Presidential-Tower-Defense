using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MonsterLevel {
  public int cost;
  public GameObject visualization;
  public GameObject bullet;
  public float fireRate;
}

public class MonsterData : MonoBehaviour {

	public string id;

	public List<MonsterLevel> levels;
	private MonsterLevel currentLevel;

	private GameObject progenitor;
	private int playerFaction;

	//1
	public MonsterLevel CurrentLevel {
		//2
		get {
			return currentLevel;
		}
		//3
		set {
			currentLevel = value;
			int currentLevelIndex = levels.IndexOf(currentLevel);
			
			GameObject levelVisualization = levels[currentLevelIndex].visualization;
			for (int i = 0; i < levels.Count; i++) {
				if (levelVisualization != null) {
					if (i == currentLevelIndex) {
						levels[i].visualization.SetActive(true);
					} else {
						levels[i].visualization.SetActive(false);
					}
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		playerFaction = PlayerPrefs.GetInt ("Faction");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetProgenitor(GameObject m)
	{
		progenitor = m;
	}

	public void setID(string nid)
	{
		id = nid;
	}

	void OnEnable() {
 		CurrentLevel = levels[0];
	}

	//index of the levels: 0- base; 1- republican; 2- democrat
	//faction variable: 1- democrat; 2- republican

	public MonsterLevel getNextLevel() {
		int currentLevelIndex = levels.IndexOf (currentLevel);
		int maxLevelIndex = levels.Count - 1;
		if (currentLevelIndex < maxLevelIndex - 1) {
			return levels[currentLevelIndex+2];
		} else {
			return null;
		}
	}
	
	public void increaseLevel() {
		/*int currentLevelIndex = levels.IndexOf(currentLevel);
		if (currentLevelIndex < levels.Count - 1) {
			CurrentLevel = levels[currentLevelIndex + 1];
		}*/
		if(CurrentLevel == levels[0]){
			if (playerFaction == 1) { // Democrat: upgrades are index 2 
				CurrentLevel = levels [2];
			} else { // Republican: upgrades are index 1
				CurrentLevel = levels [1];
			}
		}	
	}
}
