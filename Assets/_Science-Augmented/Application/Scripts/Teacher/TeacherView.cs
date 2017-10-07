using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public enum TeacherViewState{Introduction,Game,Challenge}

public class TeacherStateEvent: UnityEvent<TeacherViewState>{}

public class TeacherView : NetworkBehaviour {

	public TeacherActivity currentActivity;
	public TeacherViewState currentState;
	public static TeacherView sInstance;
	[HideInInspector]
	public TeacherStateEvent OnChangeState = new TeacherStateEvent ();

	void Awake(){
		sInstance = this;
	}
	public void StartInstruction(){

	}

	public void StartGame(){

	}

	public void EndGame(){

	}

	public void Restart(){

	}

	public void ChangeState(TeacherViewState state){
		currentState = state;
		OnChangeState.Invoke (state);
	}
		


}
