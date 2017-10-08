using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChild : MonoBehaviour
{
    public int ChildToShow;
    public bool ShowOnAwake =false;
    List<EducationModel> models;
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
        models = new List<EducationModel>(transform.GetComponentsInChildren<EducationModel>(true));
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

    public void ShowChildByKey(int key)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        EducationModel model = models.Find(e => e.Key == key);
        model.gameObject.SetActive(true);

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
