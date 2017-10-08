using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public enum TeacherViewState{Introduction,Game,Challenge}

public class TeacherStateEvent: UnityEvent<TeacherViewState>{}

public class TeacherView : MonoBehaviour {

	public TeacherActivity currentActivity;
	public TeacherViewState currentState;
	public GameObject LearningObjectivePanel;
	public static TeacherView sInstance;
	public Animator[] animators;

	[HideInInspector]
	public TeacherStateEvent OnChangeState = new TeacherStateEvent ();

	void Awake(){
		sInstance = this;
	}

	void Start(){
		
		LearningObjectivePanel.SetActive (true);
		if (currentActivity != null) {
			currentActivity.DisableView ();
		}


	}
	public void StartInstruction(){
		LearningObjectivePanel.SetActive (false);
		currentActivity.StartInstruction ();
		if(NetworkClassroomManager.sInstance)
		NetworkClassroomManager.sInstance.ShowIntroduction();

	}
	//Network
	public void StartGame(){
		LearningObjectivePanel.SetActive (false);
		currentActivity.StartGame ();
		if(NetworkClassroomManager.sInstance)
		NetworkClassroomManager.sInstance.ShowGame();
	}

	//Network
	public void EndGame(){
		LearningObjectivePanel.SetActive (false);
		if(NetworkClassroomManager.sInstance)
		NetworkClassroomManager.sInstance.StopGame ();
	}

	public void Restart(){

	}

	public void ChangeIntroductionModel(int ID){


		int[] modelValues = new int[1];
		modelValues [0] = ID;

		if(NetworkClassroomManager.sInstance)
			NetworkClassroomManager.sInstance.UpdateModels (modelValues);
	}

	public void ChangeModel(int[] ID){
		if(NetworkClassroomManager.sInstance)
		NetworkClassroomManager.sInstance.UpdateModels (ID);

	}

	public void StartAnimation(){
		for (int i = 0; i < animators.Length; i++) {
			if (animators [i].enabled) {
				animators[i].Rebind();
			}
			animators [i].enabled = true;

		}
		if(NetworkClassroomManager.sInstance)
		NetworkClassroomManager.sInstance.StartAnimation ();
	}

	public void StopAnimation(){
		
		for (int i = 0; i < animators.Length; i++) {
			animators[i].enabled = false;
		}

		if(NetworkClassroomManager.sInstance)
		NetworkClassroomManager.sInstance.StopAnimation ();
	}



	public void ChangeState(TeacherViewState state){
		currentState = state;
		OnChangeState.Invoke (state);

	    switch (state)
	    {
	            case TeacherViewState.Challenge:
	            print("Null");
	            break;
            case TeacherViewState.Introduction:
                StartInstruction();
                break;
            case TeacherViewState.Game:
	            StartGame();
	            break;
	    }
	}

	public void ChangeActivity(TeacherActivity targetActivity){
		print (targetActivity);
		currentActivity = targetActivity;
		StartInstruction ();
	}
		


}
