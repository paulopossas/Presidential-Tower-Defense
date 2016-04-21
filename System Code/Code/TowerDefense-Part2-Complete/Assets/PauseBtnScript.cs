using UnityEngine;
using System.Collections;

public class PauseBtnScript : MonoBehaviour {

	private GameManagerBehavior gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		//print ("pause");
	}

	public void togglePause()
	{
		if (gameManager.isPaused) {
			//print ("Unpausing");
			gameManager.unpause ();
		} else {
			//print ("Pausing");
			gameManager.pause ();
		}
	}

	public void pause()
	{
		
	}
}
