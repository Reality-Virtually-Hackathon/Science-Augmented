using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;

public class ModelGroup : MonoBehaviour
{

    public EducationModel ActivatedActiveModel { get; private set; }
    List<EducationModel> models = new List<EducationModel>();
    public Vector3 StartLocal;
    // Use this for initialization

    void Awake()
    {
        models = new List<EducationModel>(GetComponentsInChildren<EducationModel>(true));
    }

    void Start ()
    {
        StartLocal = transform.localPosition;
      
    }

    public bool IsEnzyme()
    {
        return Enumerable.FirstOrDefault(Enumerable.Select(models, t => t.gameObject.activeSelf && t.Enzyme));
    }

    public EducationModel GetModelByKey(int key)
    {
        return models.Find(e => e.Key == key);
    }

    public EducationModel GetActive()
    {
        return Enumerable.FirstOrDefault(Enumerable.Select(models, t => t.gameObject.activeSelf ? t : null));
    }

    public void showModel(int key)
    {
       EducationModel model =  models.Find(e => e.Key == key);
        if (model)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            model.gameObject.SetActive(true);
            ActivatedActiveModel = model;
        }
    }
    // Update is called once per frame
	void Update () {
		
	}
}
