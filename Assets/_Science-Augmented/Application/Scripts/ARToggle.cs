using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class ARToggle : MonoBehaviour {
	private bool mBackgroundWasSwitchedOff = false;

	// Use this for initialization
	void Start () {

	}

	public void EnableVuforia(bool enabled){
		VuforiaBehaviour.Instance.enabled = enabled;

	}

}
