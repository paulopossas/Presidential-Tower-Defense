using UnityEngine;
using System.Collections;

public class BuildMenuScript : MonoBehaviour {

	private Transform b1Btn;
	private Transform b2Btn;
	private Transform b3Btn;
	private Transform canBtn;
	private Transform ring;
	private bool menuVisible;

	public GameObject upgradeMenuPrefab;

	private GameObject towerPrefab;

	//private GameObject progenitor;
	private GameObject tower;
	private GameObject upgradeMenu;
	private GameManagerBehavior gameManager;

	//private bool isReady = false;

	// Use this for initialization
	void Start () {
		b1Btn = transform.FindChild("Build1");
		b2Btn = transform.FindChild("Build2");
		b3Btn = transform.FindChild("Build3");
		canBtn = transform.FindChild("Cancel");
		ring = transform.FindChild("Ring");

		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

		hideMenu ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp()
	{
		if (hasTower ()) {
			showUpgradeMenu ();
		} else {
			ShowMenu ();
		}


	}

	void showUpgradeMenu() {
		upgradeMenu.GetComponentInChildren<TowerMenuScript> ().ShowMenu ();

	}

	public void hideSelf() {
		gameObject.GetComponent<CircleCollider2D> ().radius = 0f;
	}

	public void showSelf() {
		gameObject.GetComponent<CircleCollider2D> ().radius = 0.7f;
	}



	private bool hasTower()
	{
		return (tower != null);
	}



	/*
	public void setProgenitor(GameObject p)
	{
		progenitor = p;
	}
	*/

	public void hideMenu()
	{
		b1Btn.localScale = new Vector3 (0,0,0);
		b2Btn.localScale = new Vector3 (0,0,0);
		b3Btn.localScale = new Vector3 (0,0,0);
		canBtn.localScale = new Vector3 (0,0,0);
		ring.localScale = new Vector3 (0,0,0);
		menuVisible = false;

		b1Btn.gameObject.GetComponent<CircleCollider2D> ().radius = 0;
		b2Btn.gameObject.GetComponent<CircleCollider2D> ().radius = 0;
		b3Btn.gameObject.GetComponent<CircleCollider2D> ().radius = 0;
		canBtn.gameObject.GetComponent<CircleCollider2D> ().radius = 0;

		// collider
		//transform.FindChild ("Collider").gameObject.GetComponent<CircleCollider2D> ().radius = 0.54f;
	}

	public void ShowMenu()
	{
		
		b1Btn.localScale = new Vector3 (1,1,1);
		b2Btn.localScale = new Vector3 (1,1,1);
		b3Btn.localScale = new Vector3 (1,1,1);
		canBtn.localScale = new Vector3 (1,1,1);
		ring.localScale = new Vector3 (1,1,1);

		b1Btn.gameObject.GetComponent<CircleCollider2D> ().radius = 0.4f;
		b2Btn.gameObject.GetComponent<CircleCollider2D> ().radius = 0.4f;
		b3Btn.gameObject.GetComponent<CircleCollider2D> ().radius = 0.4f;
		canBtn.gameObject.GetComponent<CircleCollider2D> ().radius = 0.4f;
		// collider
		//transform.FindChild ("Collider").gameObject.GetComponent<CircleCollider2D> ().radius = 0;


		menuVisible = true;

	}

	private bool canPlaceTower(GameObject attemptedTower) {
		int cost = attemptedTower.GetComponent<MonsterData> ().levels[0].cost;
		return tower == null && gameManager.Gold >= cost;
	}

	public void attemptBuild(GameObject attemptedTower) {
		if (canPlaceTower (attemptedTower)) {
			buildTower (attemptedTower);
		}
	}

	private void buildTower(GameObject towerPrefab)
	{

		Vector3 v = gameObject.transform.localScale;
		string id = v.x + " " + v.y;

		upgradeMenu = (GameObject)Instantiate (upgradeMenuPrefab, transform.position, Quaternion.identity);
		upgradeMenu.GetComponentInChildren<TowerMenuScript> ().setProgenitor (gameObject);

		tower = (GameObject)Instantiate (towerPrefab, transform.position, Quaternion.identity);
		upgradeMenu.GetComponentInChildren<TowerMenuScript> ().setMonster (tower);


		//tower.GetComponent<MonsterData> ().setID ("t" + id); // tower + location id
		//upgradeMenu.GetComponentInChildren<TowerMenuScript> ().setID ("m" + id); // menu + location id
		//4
		AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
		audioSource.PlayOneShot (audioSource.clip);

		gameManager.Gold -= tower.GetComponent<MonsterData> ().CurrentLevel.cost;

		hideMenu ();

		//gameObject.GetComponent<CircleCollider2D> ().radius = 0f;
	}
}
