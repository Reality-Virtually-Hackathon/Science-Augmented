using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


[RequireComponent(typeof(Button))]
public class TeacherStateButton : MonoBehaviour {

	[SerializeField]
	private TeacherViewState targetActivity;
	private Button thisButton;

	// Use this for initialization
	void Start () {
		
		thisButton = GetComponent<Button> ();
		thisButton.onClick.AddListener (UpdateState);
		//TeacherView.sInstance.OnChangeState.AddListener (UpdateButtonGraphic);

	}


	void UpdateState(){
		TeacherView.sInstance.ChangeState (targetActivity);
	}


}
