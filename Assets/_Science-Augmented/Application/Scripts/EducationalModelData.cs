using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EducationalModelData : MonoBehaviour {

    public List<EducationModel> EducationModels = new List<EducationModel>();

    public EducationModel GetEducationModelById(string id)
    {
        EducationModel model = null;
        model = EducationModels.Find(e => string.CompareOrdinal(e.Key, id) == 0);
        return model;

    }

    public EducationModel GetEducationModelByGameObject(GameObject model)
    {
        EducationModel educationModel = null;
        educationModel = EducationModels.Find(e => e.gameObject == model);
        return educationModel;

    }
}
