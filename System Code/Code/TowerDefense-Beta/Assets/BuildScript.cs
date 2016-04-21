using UnityEngine;
using System.Collections;

public class BuildScript : MonoBehaviour {

	public GameObject towerPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp() {

		//print ("Ok");

		transform.parent.transform.GetComponent<BuildMenuScript> ().attemptBuild(towerPrefab);
	}


}
