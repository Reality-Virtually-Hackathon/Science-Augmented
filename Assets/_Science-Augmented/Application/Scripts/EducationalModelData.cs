using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EducationalModelData : MonoBehaviour {

    public List<EducationModel> EducationModels = new List<EducationModel>();

    public EducationModel GetEducationModelById(int id)
    {
        EducationModel model = null;
        model = EducationModels.Find(e => e.Key == id);
        return model;

    }

    public EducationModel GetEducationModelByGameObject(GameObject model)
    {
        EducationModel educationModel = null;
        educationModel = EducationModels.Find(e => e.gameObject == model);
        return educationModel;

    }
}
