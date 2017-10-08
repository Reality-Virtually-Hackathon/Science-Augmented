using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShowModelsButton : MonoBehaviour {

	[SerializeField]
	private int[] keys;
	private Button thisButton;

	// Use this for initialization
	void Start () {

		thisButton = GetComponent<Button> ();
		thisButton.onClick.AddListener (UpdateState);//TeacherView.sInstance.OnChangeState.AddListener (UpdateButtonGraphic);

	}


	void UpdateState(){
		TeacherView.sInstance.ChangeModel(keys);
	}

}