using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public List<Renderer> targets;
    private Color startColor;
	// Use this for initialization
	void Start ()
	{
	    if(targets.Count > 0 && targets[0] != null && targets[0].material)
	    {
	        startColor = targets[0].material.color;
	    }
	    else
	    {
	        startColor = Color.white;
	    }
	}

    public void ChangeToColor(Color changeColor, float time)
    {
        StartCoroutine(StartColorChange(changeColor, time));
    }

    public void SetBackToStartColor(float time)
    {
        StopAllCoroutines();
        ChangeToColor(startColor,time);
    }

    private IEnumerator StartColorChange( Color changeColor, float time)
    {
        

        float elapsedTime = 0;
       

        while (elapsedTime < time)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                Color newColor = targets[i].material.color;
                newColor = Color.Lerp( newColor , changeColor, ( elapsedTime / time ));
                targets[i].material.color = newColor;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
