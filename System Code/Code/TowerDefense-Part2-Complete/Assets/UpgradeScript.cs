using UnityEngine;
using System.Collections;

public class UpgradeScript : MonoBehaviour {

	public string towername;
	public string towertype;
	public float damage;
	public float upgradeCost;
	public GUIStyle tooltipStyle;

	string upgradeText;



	int[] labelcoords = {-100,-100,100,60};

	int tooltipDelay = 0;
	bool canHideTooltip = true;
	bool istooltipVisible = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(istooltipVisible) {
			if (canHideTooltip) {
				if (tooltipDelay > 0) {
					--tooltipDelay;
				} else {
					hideTooltip ();
				}
			}
		} 
	}

	void OnGUI()
	{
		//GUI.Button(new Rect(btncoords[0], btncoords[1], btncoords[2], btncoords[3]), new GUIContent("Click me", upgradeText));
		upgradeText = towername + "\nType: " + towertype + "\nDamage: " + damage + "\nCost: " + upgradeCost;

		var textArea = new Rect(labelcoords[0], labelcoords[1], labelcoords[2], labelcoords[3]);
		GUI.Box (new Rect (labelcoords [0], labelcoords [1], labelcoords [2], labelcoords [3]),"");

		GUI.Label(textArea, upgradeText, tooltipStyle);

		//GUI.tooltip = upgradeText;
	}

	void OnMouseUp() {
		bool isRepublican = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>().isRepublican;

		// I'm not sure how Republican and Democrat towers are distinguished right now.
		// But call a different method in the TowerMenuScript based on which faction the player is.
		if (isRepublican) {

		}
		hideTooltip ();
		transform.parent.transform.GetComponentInChildren<TowerMenuScript> ().upgrade ();

	}

	void OnMouseExit() {
		
		//print ("exited");
		canHideTooltip = true;


	}

	void hideTooltip()
	{
		labelcoords [0] = -100;
		labelcoords [1] = -100;
		tooltipDelay = 0;
		istooltipVisible = false;
	}

	void OnMouseEnter() {
		//print ("Entered");
		labelcoords [0] = (int) Input.mousePosition.x + 10;
		labelcoords [1] = (int) Input.mousePosition.y - 60;

		//print ("mouse: " + Input.mousePosition.x + ", " + Input.mousePosition.y);
		//print ("tools: " + labelcoords[0] + ", " + labelcoords[1]);

		tooltipDelay = 30;
		canHideTooltip = false;
		istooltipVisible = true;

	}


}
