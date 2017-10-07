using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentArController : MonoBehaviour
{
    public List<Vuforia.ImageTargetBehaviour> ImageTargetPrefabs;
    
    private List<GameObject> objectAnchors = new List<GameObject>();
    [SerializeField]
    private List<GameObject> ArObjectModels = new List<GameObject>();
 
    
    // Use this for initialization
    void Start () {
        CreateModels();
    }

    public void SetModels(GameObject[] models)
    {
        List<GameObject> modelList = new List<GameObject>();
        for (int i = 0; i < models.Length; i++)
        {
            modelList.Add(models[i]);
        }
        SetModels(modelList);

    }

    public void SetModels(List<GameObject> models)
    {
        ArObjectModels = models;
    }


    public void CreateModels()
    {
        for (int i = 0; i < ArObjectModels.Count; i++)
        {
            int imageTargetNumber = i % (ImageTargetPrefabs.Count - 1);
            Vuforia.ImageTargetBehaviour imageTarget = Instantiate(ImageTargetPrefabs[imageTargetNumber]);
            objectAnchors.Add(imageTarget.transform.GetChild(0).gameObject);
            GameObject arObject = Instantiate(ArObjectModels[i], objectAnchors[i].transform);
            arObject.transform.localPosition = Vector3.zero;
            
            arObject.transform.localScale = Vector3.one/2;
        }
        
    }

    // Update is called once per frame
	void Update () {
		
	}
}
