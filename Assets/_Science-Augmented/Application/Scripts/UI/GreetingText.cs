using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GreetingText : MonoBehaviour {
	public string name = "Mr.Hope";
	// Use this for initialization
	void Start () {
		System.DateTime time = System.DateTime.Now;
		int hour = time.Hour;
		string greeting;

		if (hour < 12) {
			greeting = "Good Morning! " + name;
		} else if (hour < 18) {
			greeting = "Good Afternoon! " + name;
		} else {
			greeting = "Good Evening! " + name;
		}


		GetComponent<TextMeshProUGUI> ().text = greeting;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
