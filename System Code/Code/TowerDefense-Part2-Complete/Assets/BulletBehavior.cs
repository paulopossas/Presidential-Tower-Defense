using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletBehavior : MonoBehaviour {

	public float speed = 10;
	public float aoeRange = 0;
	public float damage;
	public float splashFactor = 0.5f;
	public GameObject target;
	public Vector3 startPosition;
	public Vector3 targetPosition;

	public List<GameObject> enemiesInRange;
	
	private float distance;
	private float startTime;
	
	private GameManagerBehavior gameManager;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		distance = Vector3.Distance (startPosition, targetPosition);
		GameObject gm = GameObject.Find("GameManager");
		gameManager = gm.GetComponent<GameManagerBehavior>();	

		enemiesInRange = new List<GameObject>();

		if (aoeRange > 0) {
			GetComponent<CircleCollider2D> ().radius = aoeRange;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// 1 
		if (target != null) {
			targetPosition = target.transform.position;
		}

		// rotate bullet to face target
		float distanceX = targetPosition.x - transform.position.x;
		float distanceY = targetPosition.y - transform.position.y;
		float distanceTotal = Mathf.Sqrt (distanceX * distanceX + distanceY * distanceY);

		float k = transform.rotation.z;

		transform.Rotate (new Vector3 (0, 0, k - Mathf.Asin (distanceX / distanceTotal)));

		float timeInterval = Time.time - startTime;
		gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);


		// 2 
		if (gameObject.transform.position.Equals(targetPosition)) {
			AssignDamage ();
			Destroy(gameObject);
		}	
	}

	public void setStats(float spd, float dmg, float aoe, float splash)
	{
		speed = spd;
		damage = dmg;
		aoeRange = aoe;
		splashFactor = splash;
	}

	void AssignDamage()
	{
		//print ("Assigning damage");

		if (target != null) {


			DealDamage (target,damage);

			if (aoeRange > 0) {
				enemiesInRange.ForEach (DealSplashDamage);
			}
		}
	}

	void DealDamage(GameObject objectHit, float damageDealt)
	{
		if (objectHit != null) {
			// 3
			Transform healthBarTransform = objectHit.transform.FindChild ("HealthBar");
			HealthBar healthBar = 
				healthBarTransform.gameObject.GetComponent<HealthBar> ();
			healthBar.currentHealth -= Mathf.Max (damageDealt, 0);
			// 4
			if (healthBar.currentHealth <= 0) {
				Destroy (objectHit);
				AudioSource audioSource = objectHit.GetComponent<AudioSource> ();
				AudioSource.PlayClipAtPoint (audioSource.clip, transform.position);

				gameManager.Gold += 50;
			}
		}
	}

	void DealSplashDamage(GameObject objectHit)
	{
		if (objectHit != target) {
			DealDamage (objectHit, damage * splashFactor);
			//print ("Splash hit");
		}

	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag.Equals("Enemy")) {
			enemiesInRange.Add(other.gameObject);
			//print ("Entered range");

		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag.Equals("Enemy")) {
			enemiesInRange.Remove(other.gameObject);
			//print ("exited range");

		}
	}
}
