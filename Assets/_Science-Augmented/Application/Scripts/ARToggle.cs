using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_WEBGL
using Vuforia;
#endif
public class ARToggle : MonoBehaviour {
	private bool mBackgroundWasSwitchedOff = false;

	// Use this for initialization
	void Start () {

	}

	public void EnableVuforia(bool enabled){
		#if !UNITY_WEBGL
	    if(VuforiaBehaviour.Instance)
		VuforiaBehaviour.Instance.enabled = enabled;
		#endif

	}

}
