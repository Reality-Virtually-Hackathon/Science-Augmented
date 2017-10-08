using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpChildPosition : MonoBehaviour
{

    Vector3 startPosition;
    Transform child;
    public bool SetSelectedChild = false;
    public int SelectedChild = 0;
    private bool setToPosition;
	// Use this for initialization
	void Start () {
	    if (SetSelectedChild)
	    {
	        child = transform.GetChild(SelectedChild);

            startPosition = child.localPosition;
	    }
	    else
	    {
	        for (int i = 0; i < transform.childCount; i++)
	        {
	            if (transform.GetChild(i).gameObject.activeSelf)
	            {
                    child = transform.GetChild(i);
                    startPosition = child.localPosition;
	                break;
	            }
	        }
	    }
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
