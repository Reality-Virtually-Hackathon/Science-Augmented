using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpChildPosition : MonoBehaviour
{

    Vector3 startPosition;
    public Transform child;
    private bool setToPosition;
	// Use this for initialization
	void Start ()
	{

	   
	    

	        child = transform;
	        startPosition = transform.localPosition;
	    
	}

    public void StartLerpTo(Vector3 localPosition, float lerpTime = 10)
    {
        StopAllCoroutines();
        setToPosition = false;
        StartCoroutine(StartSetInPlace(localPosition, lerpTime));
    }

    public void LerpBackToStart(float lerpTime = 10)
    {
        StopAllCoroutines();
        setToPosition = false;
        StartCoroutine(StartSetInPlace(startPosition, lerpTime));
    }
    
    private IEnumerator StartSetInPlace(Vector3 localPosition, float time = 2)
    {
    

        float elapsedTime = 0;


        while (elapsedTime < time)
        {
            child.localPosition = Vector3.Lerp(child.localPosition, localPosition, ( elapsedTime / time ));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        setToPosition = true;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
