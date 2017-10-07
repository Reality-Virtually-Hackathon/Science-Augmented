using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;

public enum TeacherViewState{Introduction,Game,Challenge}

public class TeacherStateEvent: UnityEvent<TeacherViewState>{}

public class TeacherView : NetworkBehaviour {

	public TeacherActivity currentActivity;
	public TeacherViewState currentState;
	public static TeacherView sInstance;
	public Text RoomName;

	[HideInInspector]
	public TeacherStateEvent OnChangeState = new TeacherStateEvent ();

	void Awake(){
		sInstance = this;
	}

	void Start(){
		RoomName.text = "Room Name";
	}
	public void StartInstruction(){
		currentActivity.StartInstruction ();
		NetworkClassroomManager.sInstance.ShowIntroduction();

	}
	//Network
	public void StartGame(){
		currentActivity.StartGame ();
		NetworkClassroomManager.sInstance.ShowGame();
	}

	//Network
	public void EndGame(){
		NetworkClassroomManager.sInstance.StopGame ();
	}

	public void Restart(){

	}


	public void ChangeModel(int[] ID){
		int numberOfPlayers = Network.connections.Length;
		int[] modelValues = new int[numberOfPlayers];
		int currentIndex= 0;

		for (int i = 0; i < numberOfPlayers; i++) {
			if (currentIndex < ID.Length) {
				modelValues [i] = ID [currentIndex];
				currentIndex ++;
			} else {
				currentIndex = 0;
				modelValues [i] = ID [currentIndex];
			}
		}
		NetworkClassroomManager.sInstance.UpdateModels (modelValues);

	}

	public void StartAnimation(){
		NetworkClassroomManager.sInstance.StartAnimation ();
	}

	public void StopAnimation(){
		NetworkClassroomManager.sInstance.StopAnimation ();
	}



	public void ChangeState(TeacherViewState state){
		currentState = state;
		OnChangeState.Invoke (state);
	}

	public void ChangeActivity(TeacherActivity targetActivity){
		currentActivity = targetActivity;
	}
		


}
