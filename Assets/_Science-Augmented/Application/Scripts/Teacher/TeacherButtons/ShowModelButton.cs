using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShowModelButton : MonoBehaviour {
	
	public EducationModel targetModel;
	private Button thisButton;
	public static EducationModel currentTargetModel ;
	// Use this for initialization
	void Start () {

		thisButton = GetComponent<Button> ();
		thisButton.onClick.AddListener (UpdateState);
		//TeacherView.sInstance.OnChangeState.AddListener (UpdateButtonGraphic);

	}


	void UpdateState(){
		if (currentTargetModel != null)
			currentTargetModel.gameObject.SetActive (false);
		
		TeacherView.sInstance.ChangeIntroductionModel(targetModel.Key);
		currentTargetModel = targetModel;

		if (currentTargetModel != null)
			currentTargetModel.gameObject.SetActive (true);
	}
}
