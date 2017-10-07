using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayerModelsEvent :UnityEvent<int[]>{}
public class NetworkClassroomManager : NetworkBehaviour {

	static public List<NetworkPlayer> splayers = new List<NetworkPlayer>();
	static public NetworkClassroomManager sInstance = null;
	public PlayerModelsEvent OnPlayerModelsChange = new PlayerModelsEvent();

	[HideInInspector]
	public UnityEvent OnShowGame = new UnityEvent();
	[HideInInspector]
	public UnityEvent OnShowIntoduction = new UnityEvent();
	[HideInInspector]
	public UnityEvent OnStopGame = new UnityEvent();
	[HideInInspector]
	public UnityEvent OnStudentComplete = new UnityEvent();
	[HideInInspector]
	public UnityEvent OnStartAnimation = new UnityEvent();
	[HideInInspector]
	public UnityEvent OnStopAnimation = new UnityEvent();

	protected bool _running = true;

	void Awake()
	{
		sInstance = this;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	[ServerCallback]
	void Update()
	{
		if (!_running)
			return;

	}

	IEnumerator ReturnToLoby()
	{
		_running = false;
		yield return new WaitForSeconds(3.0f);
		LobbyManager.s_Singleton.ServerReturnToLobby();
	}

	public IEnumerator WaitForRespawn(NetworkSpaceship ship)
	{
		yield return new WaitForSeconds(4.0f);

		ship.Respawn();
	}

	public void UpdateModels(int[] models){
		RPCUpdateModels (models);
	}

	public void ShowIntroduction(){
		RPCShowIntroduction ();
	}

	public void ShowGame(){
		RPCShowGame ();
	}

	public void StopGame(){
		RPCShowGame ();
	}

	public void Complete(){
		RPCShowGame ();
	}

	public void StartAnimation(){
		RPCStartAnimation ();
	}

	public void StopAnimation(){
		RPCStopAnimation ();
	}

	[ClientRpc]
	public void RPCStopAnimation(){
		Debug.Log ("Stop Animation");
		OnStopAnimation.Invoke ();
	}

	[ClientRpc]
	public void RPCStartAnimation(){
		Debug.Log ("Show Animation");
		OnStartAnimation.Invoke ();
	}

	[ClientRpc]
	public void RPCStopGame(){
		Debug.Log ("Show End");
		OnStopGame.Invoke ();
	}
	[ClientRpc]
	public void RPCStudentComplete(){
		Debug.Log ("Add Complete");
		OnStudentComplete.Invoke ();
	}
	[ClientRpc]
	public void RPCShowIntroduction(){
		Debug.Log ("Show Intro");
		OnShowIntoduction.Invoke ();
	}
	[ClientRpc]
	public void RPCShowGame(){
		Debug.Log ("Show Game");
		OnShowGame.Invoke ();
	}

	[ClientRpc]
	public void RPCUpdateModels(int[] models){
		Debug.Log (models.Length);
		OnPlayerModelsChange.Invoke (models);

	}
}
