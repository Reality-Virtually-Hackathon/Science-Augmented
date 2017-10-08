using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ClassInfoText : MonoBehaviour {
	public string ClassName = "Biology Class";

	// Use this for initialization
	void Start () {
		string prefix;
		int time;
		int hours = System.DateTime.Now.Hour;
		if (hours<12) {
			time = hours;
			prefix = "am";
		} else {
			time = hours-12;
			prefix = "pm";
		}
		string description = ClassName + " @ " + time+prefix;
		GetComponent<TextMeshProUGUI> ().text = description;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
