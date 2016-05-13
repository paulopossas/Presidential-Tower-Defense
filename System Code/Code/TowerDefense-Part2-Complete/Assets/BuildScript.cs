using UnityEngine;
using System.Collections;

public class BuildScript : MonoBehaviour {

	public string towername;
	public string towertype;
	public float damage;
	public float buildCost;

	string buildText;

	int tooltipDelay = 0;
	bool canHideTooltip = true;
	bool istooltipVisible = false;
	public GUIStyle tooltipStyle;
	int[] labelcoords = {-100,-100,90,60};

	public GameObject towerPrefab;



	// Use this for initialization
	void Start () {
		towername = towerPrefab.GetComponent<MonsterData> ().levels [0].name;
		towertype = towerPrefab.GetComponent<MonsterData> ().levels [0].towerType;
		damage = towerPrefab.GetComponent<MonsterData> ().levels [0].damage;
		buildCost = towerPrefab.GetComponent<MonsterData> ().levels [0].cost;
	}
	
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
		buildText = towername + "\nType: " + towertype + "\nDamage: " + damage + "\nCost: " + buildCost;

		var textArea = new Rect(labelcoords[0], labelcoords[1], labelcoords[2], labelcoords[3]);
		GUI.Box (new Rect (labelcoords [0], labelcoords [1], labelcoords [2], labelcoords [3]),"");

		GUI.Label(textArea, buildText, tooltipStyle);

		//GUI.tooltip = upgradeText;
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
		labelcoords [0] = (int) Input.mousePosition.x + 20;
		labelcoords [1] = (int) Input.mousePosition.y - labelcoords [3];
		//print (Input.mousePosition.x);
		//print (Input.mousePosition.y);
		tooltipDelay = 4;
		canHideTooltip = false;
		istooltipVisible = true;

	}

	void OnMouseUp() {

		//print ("Ok");
		hideTooltip();
		transform.parent.transform.GetComponent<BuildMenuScript> ().attemptBuild(towerPrefab);
	}


}
