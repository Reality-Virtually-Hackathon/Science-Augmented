using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChild : MonoBehaviour
{
    public int ChildToShow;
    public bool ShowOnAwake =false;
	// Use this for initialization
	void Awake () {

	    for (int i = 0; i < transform.childCount; i++)
	    {
	        transform.GetChild(i).gameObject.SetActive(true);
	    }
	    if (ShowOnAwake)
	    {
	        ShowChildIndex(ChildToShow);
	    }
	}

    void Start()
    {
        TurnOff();
    }

    public void TurnOff()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ShowChildIndex(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        int childIndex = index % transform.childCount;
        transform.GetChild(childIndex).gameObject.SetActive(true);
    }
}
