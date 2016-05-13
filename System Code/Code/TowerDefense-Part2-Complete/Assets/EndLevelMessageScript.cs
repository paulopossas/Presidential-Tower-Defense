using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelMessageScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int beatenLevel = PlayerPrefs.GetInt ("CurrentLevel");

		string messageTxt = "";
		switch (beatenLevel) {
		case 1:
			messageTxt = "Congratulations, you have been found " +
			"to be a successful candidate. The people truly adore " +
			"you! Will you continue on the campaign trail?";
			break;
		case 2:
			messageTxt = "The Primaries have been a success!  Do you " +
				"have what it takes to win the presidency? America is " +
				"depending on you...";
			break;

		case 3:
			messageTxt = "Congratulations, you fought your way through a " +
				"grueling up-hill battle in the American political system. " +
				"You will make a truly honorable President.\n";
			break;

		case 0:
			messageTxt = "Intruder Alert! Scramble the TIE Fighters!";
			break;

		default:
			messageTxt = "A Winner is You!";
			break;
		}
			
		gameObject.GetComponent<Text> ().text = messageTxt;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
