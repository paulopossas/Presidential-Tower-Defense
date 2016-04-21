using UnityEngine;
using System.Collections;

public class UpgradeScript : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp() {
		bool isRepublican = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>().isRepublican;

		// I'm not sure how Republican and Democrat towers are distinguished right now.
		// But call a different method in the TowerMenuScript based on which faction the player is.
		if (isRepublican) {

		}

		transform.parent.transform.GetComponentInChildren<TowerMenuScript> ().upgrade ();
	}

	void OnMouseEnter() {
		// display tooltip?
	}
}
