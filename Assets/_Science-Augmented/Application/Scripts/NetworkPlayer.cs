using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {
	
	public bool isTeacher;

	void Awake()
	{
		DontDestroyOnLoad (this);
		//register the spaceship in the gamemanager, that will allow to loop on it.
		NetworkClassroomManager.splayers.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
