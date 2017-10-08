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
    public UnityEvent OnStudentDestroy = new UnityEvent();
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
		RpcUpdateModels (models);
	}

	public void ShowIntroduction(){
		RpcShowIntroduction ();
	}

	public void ShowGame(){
		RpcShowGame ();
	}

	public void StopGame(){
		RpcShowGame ();
	}

	public void Complete(){
		RpcShowGame ();
	}

	public void StartAnimation(){
		RpcStartAnimation ();
	}

	public void StopAnimation(){
		RpcStopAnimation ();
	}

    public void StudentComplete()
    {
        RpcStudentComplete();
    }

    public void StudentDestroy()
    {
        RpcStudentDestroy();
    }

    //[ClientRpc]
    public void RpcStopAnimation(){
		Debug.Log ("Stop Animation");
		OnStopAnimation.Invoke ();
	}

	[ClientRpc]
	public void RpcStartAnimation(){
		Debug.Log ("Show Animation");
		OnStartAnimation.Invoke ();
	}

	[ClientRpc]
	public void RpcStopGame(){
		Debug.Log ("Show End");
		OnStopGame.Invoke ();
	}
	[ClientRpc]
	public void RpcStudentComplete(){
		Debug.Log ("Add Complete");
		OnStudentComplete.Invoke ();
	}

    [ClientRpc]
    public void RpcStudentDestroy()
    {
        Debug.Log("Add destroy");
        OnStudentDestroy.Invoke();
    }
    [ClientRpc]
	public void RpcShowIntroduction(){
		Debug.Log ("Show Intro");
		OnShowIntoduction.Invoke ();
	}
	[ClientRpc]
	public void RpcShowGame(){
		Debug.Log ("Show Game");
		OnShowGame.Invoke ();
	}

	[ClientRpc]
	public void RpcUpdateModels(int[] models){
		Debug.Log (models.Length);
		for (int i = 0; i < models.Length; i++) {
			Debug.Log (models[i]);
		}
		OnPlayerModelsChange.Invoke (models);

	}
}
