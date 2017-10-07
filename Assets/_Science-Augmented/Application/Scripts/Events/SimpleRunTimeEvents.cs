using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SimpleRunTimeEvents : MonoBehaviour {

	public UnityEvent OnStart;
	// Use this for initialization
	void Start () {
		OnStart.Invoke ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
