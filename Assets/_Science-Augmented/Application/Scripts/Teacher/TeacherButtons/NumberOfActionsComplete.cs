using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NumberOfActionsComplete : MonoBehaviour {

	private int scores;
	private Text thisText;

	// Use this for initialization
	void Start () {
		thisText.GetComponent<Text> ();
		NetworkClassroomManager.sInstance.OnStudentComplete.AddListener (AddPoints);
		NetworkClassroomManager.sInstance.OnStudentDestroy.AddListerner (RemovePoints);
	}

	void AddPoints(){
		scores++;
		thisText.text = "There are " + scores + " Reactions Complete";
	}

	void RemovePoints(){
		scores--;
		thisText.text = "There are " + scores + " Reactions Complete";
	}
}
