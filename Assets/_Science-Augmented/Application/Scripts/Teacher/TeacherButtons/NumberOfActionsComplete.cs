using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NumberOfActionsComplete : MonoBehaviour {

	private int scores;
	private Text thisText;
	private NetworkClassroomManager manager;

	void Start(){
		manager = NetworkClassroomManager.sInstance;
	}

	// Use this for initialization
	void Update () {
		thisText = transform.GetComponent<Text> ();
		thisText.text = "There are " + manager.score + " Reactions Complete";


	}

}
