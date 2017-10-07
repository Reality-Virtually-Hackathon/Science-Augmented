using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentArController : MonoBehaviour
{
    public GameObject ObjectAnchor;
    public GameObject ArObjectPrefab;
    // Use this for initialization
    void Start () {
        if (!ObjectAnchor.activeSelf)
        {
            ObjectAnchor.SetActive(true);
        }
        for (int i = 0; i < ObjectAnchor.transform.childCount; i++)
        {
            Destroy(ObjectAnchor.transform.GetChild(0).gameObject);
        }
        CreateModel();
    }

    public void CreateModel()
    {
        GameObject arObject = Instantiate(ArObjectPrefab, ObjectAnchor.transform);
        arObject.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
	void Update () {
		
	}
}
