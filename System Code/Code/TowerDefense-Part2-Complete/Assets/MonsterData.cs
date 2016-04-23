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
		int currentLevelIndex = levels.IndexOf(currentLevel);
		if (currentLevelIndex < levels.Count - 1) {
			CurrentLevel = levels[currentLevelIndex + 1];
		}
	}
}
