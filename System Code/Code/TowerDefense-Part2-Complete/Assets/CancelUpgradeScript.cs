using UnityEngine;
using System.Collections;

public class CancelUpgradeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp() {
		transform.parent.transform.GetComponentInChildren<TowerMenuScript> ().cancel ();
	}
}
