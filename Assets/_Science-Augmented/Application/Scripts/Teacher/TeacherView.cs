using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public enum TeacherViewState{Introduction,Game,Challenge}

public class TeacherStateEvent: UnityEvent<TeacherViewState>{}

public class TeacherView : NetworkBehaviour {

	public TeacherActivity currentActivity;
	public TeacherViewState currentState;
	public GameObject LearningObjectivePanel;
	public static TeacherView sInstance;
	public TextMeshProUGUI StudentCount;

	[HideInInspector]
	public TeacherStateEvent OnChangeState = new TeacherStateEvent ();

	void Awake(){
		sInstance = this;
	}

	void Start(){
		StudentCount.text = "(" + Network.connections.Length + ")";
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


		int players = Network.connections.Length;
		int[] modelValues = new int[players];
		int currentIndex= 0;

		for (int i = 0; i < players; i++) {
			modelValues [i] = ID;
		}

		if(NetworkClassroomManager.sInstance)
			NetworkClassroomManager.sInstance.UpdateModels (modelValues);
	}

	public void ChangeModel(int[] ID){
		int players = Network.connections.Length;
		int[] modelValues = new int[players];
		int currentIndex= 0;

		for (int i = 0; i < players; i++) {
			if (currentIndex < ID.Length) {
				modelValues [i] = ID [currentIndex];
				currentIndex ++;
			} else {
				currentIndex = 0;
				modelValues [i] = ID [currentIndex];
			}
		}
		if(NetworkClassroomManager.sInstance)
		NetworkClassroomManager.sInstance.UpdateModels (modelValues);

	}

	public void StartAnimation(){
		if(NetworkClassroomManager.sInstance)
		NetworkClassroomManager.sInstance.StartAnimation ();
	}

	public void StopAnimation(){
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
