using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityTest.IntegrationTestRunner;

public class TowerMenuScript : MonoBehaviour {

	//public string id;

	private GameManagerBehavior gameManager;

	private int counter = 30;

	private Transform upBtn;
	private Transform canBtn;
	private Transform ring;
	private bool menuVisible;

	public GameObject progenitor;

	public bool isReady = false;

	public GameObject monsterPrefab;
	public GameObject monster;



	// Use this for initialization
	void Start () {

	}

	void Awake() {

		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

		upBtn = transform.parent.transform.FindChild("Upgrade");
		canBtn = transform.parent.transform.FindChild("Cancel");
		ring = transform.parent.transform.FindChild("Ring");
		hideMenu ();



		//transform.parent.GetComponent<Button>().onClick.AddListener(() => { OnMouseUp(); OnMouseUp(); }); 

	}

	public void setProgenitor(GameObject p)
	{
		progenitor = p;
	}

	/*
	public void setID(string nid)
	{
		id = nid;
	}
	*/

	public void hideMenu()
	{
		//print ("hiding menu");
		upBtn.localScale = new Vector3 (0,0,0);
		canBtn.localScale = new Vector3 (0,0,0);
		ring.localScale = new Vector3 (0,0,0);
		menuVisible = false;

		upBtn.gameObject.GetComponent<CircleCollider2D> ().radius = 0;
		canBtn.gameObject.GetComponent<CircleCollider2D> ().radius = 0;

		// collider
		//transform.parent.transform.FindChild ("Collider").gameObject.GetComponent<CircleCollider2D> ().radius = 0.54f;

		if (isReady) {
			//print ("Ready");
			progenitor.GetComponent<BuildMenuScript>().showSelf();
		} else {
			//print ("not ready");
			isReady = true;
		}
	}

	public void ShowMenu()
	{
		//print ("Menu clicked");
		upBtn.localScale = new Vector3 (1,1,1);
		canBtn.localScale = new Vector3 (1,1,1);
		ring.localScale = new Vector3 (1,1,1);

		upBtn.gameObject.GetComponent<CircleCollider2D> ().radius = 0.4f;
		canBtn.gameObject.GetComponent<CircleCollider2D> ().radius = 0.4f;
		// collider
		//transform.parent.transform.FindChild ("Collider").gameObject.GetComponent<CircleCollider2D> ().radius = 0;


		menuVisible = true;

		progenitor.GetComponent<BuildMenuScript> ().hideSelf ();
	}

	public void setMonster(GameObject m)
	{
		monster = m;
	}

	public bool hasMonster()
	{
		return (monster == null);
	}

	void OnMouseUp()
	{
		if (!hasMonster ()) {
			// bring up menu to build tower
		} else {

			if (!menuVisible) {
				ShowMenu ();
			}

		}
	}

	// Update is called once per frame
	void Update () {


		if (counter <= 0) {
			counter = 200;

		}
		if ((counter % 10 == 0)) {

		}

		--counter;
	}

	private bool canUpgradeMonster() {

		if (hasMonster()) {
		} else {
			MonsterData monsterData = monster.GetComponent<MonsterData> ();
			MonsterLevel nextLevel = monsterData.getNextLevel ();
			if (nextLevel != null) {
				return gameManager.Gold >= nextLevel.cost;
			} else {
				hideMenu ();
			}
		}


		return false;
	}

	private void upgradeMonster()
	{
		if (canUpgradeMonster ()) {

			monster.GetComponent<MonsterData> ().increaseLevel ();
			//AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
			//audioSource.PlayOneShot (audioSource.clip);

			gameManager.Gold -= monster.GetComponent<MonsterData> ().CurrentLevel.cost;
		} else {
		}
	}


	public void upgrade()
	{
		// call upgradeMonster in parent
		if (canUpgradeMonster ()) {
			upgradeMonster ();
			AudioSource audio = GameObject.Find ("Menu").GetComponent<AudioSource> ();
			audio.PlayOneShot (audio.clip);
			hideMenu ();
		}


	}

	public void cancel()
	{
		hideMenu ();
	}
}
