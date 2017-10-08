﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChild : MonoBehaviour
{
    public int ChildToShow;
    public bool ShowOnStart =false;
	// Use this for initialization
	void Start () {
	 
	    if (ShowOnStart)
	    {
	        ShowChildIndex(ChildToShow);
	    }
	}

    public void ShowChildIndex(int index)
    {
        int childIndex = index % transform.childCount;
        transform.GetChild(childIndex).gameObject.SetActive(true);
    }
}
