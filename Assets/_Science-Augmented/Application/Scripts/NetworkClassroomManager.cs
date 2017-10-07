using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;

public class NetworkClassroomManager : NetworkBehaviour {

	static public List<NetworkPlayer> splayers = new List<NetworkPlayer>();
	static public NetworkClassroomManager sInstance = null;

	public string teacherScene;
	public string studentString;

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
}
