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
		transform.parent.transform.GetComponentInChildren<TowerMenuScript> ().upgrade ();
	}

	void OnMouseOver() {
		// display tooltip?
	}
}
