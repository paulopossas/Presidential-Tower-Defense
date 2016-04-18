using UnityEngine;
using System.Collections;

public class PlaceMonster : MonoBehaviour {

	public GameObject upgradeMenuPrefab;
	public GameObject monsterPrefab;

	private GameObject menu;
	private GameObject monster;
	private GameManagerBehavior gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private bool canPlaceMonster() {
		int cost = monsterPrefab.GetComponent<MonsterData> ().levels[0].cost;
		return monster == null && gameManager.Gold >= cost;
	}

	private bool hasMonster()
	{
		return (monster != null);
	}
	
	//1
	void OnMouseUp () {
  		//2
		if (!hasMonster ()) {
			if (canPlaceMonster ()) {
				//3
				Vector3 v = gameObject.transform.localScale;
				string id = v.x + " " + v.y;

				menu = (GameObject)Instantiate (upgradeMenuPrefab, transform.position, Quaternion.identity);
				menu.GetComponentInChildren<TowerMenuScript> ().setProgenitor (gameObject);

				monster = (GameObject)Instantiate (monsterPrefab, transform.position, Quaternion.identity);
				menu.GetComponentInChildren<TowerMenuScript> ().setMonster (monster);

				monster.GetComponent<MonsterData> ().setID ("t" + id); // tower + location id
				menu.GetComponentInChildren<TowerMenuScript> ().setID ("m" + id); // menu + location id
				//4
				AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
				audioSource.PlayOneShot (audioSource.clip);

				gameManager.Gold -= monster.GetComponent<MonsterData> ().CurrentLevel.cost;

			} else {
				// unable to place monster

			}
		} else {
			menu.GetComponentInChildren<TowerMenuScript> ().ShowMenu ();

		}
	}

	public void hide()
	{
		gameObject.transform.localScale = new Vector3 (0, 0, 0);
	}

	public void show()
	{
		gameObject.transform.localScale = new Vector3 (4,4,1);
	}

	/*
	private bool canUpgradeMonster() {
		if (monster != null) {
			MonsterData monsterData = monster.GetComponent<MonsterData> ();
			MonsterLevel nextLevel = monsterData.getNextLevel();
			if (nextLevel != null) {
				return gameManager.Gold >= nextLevel.cost;
 			}
  		}
		return false;
	}
	*/
}
