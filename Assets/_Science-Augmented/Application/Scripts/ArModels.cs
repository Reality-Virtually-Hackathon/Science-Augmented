using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class ArModels  {

    public Dictionary<string, EducationModel> arModelDictionary = new Dictionary<string, EducationModel>();
    
    private List<GameObject> arObjectModels = new List<GameObject>();
    private readonly List<string> arObjectKeys = new List<string>();

    /// <summary>
    /// Overrides all the keys, but sets the models of the dictionary
    /// </summary>
    /// <param name="models"></param>
    public void SetModels(GameObject[] models)
    {
        List<GameObject> modelList = models.ToList();
        SetModels(modelList);

    }

    /// <summary>
    /// Overrides all the keys, but sets the models of the dictionary
    /// </summary>
    /// <param name="models"></param>
    public void SetModels(List<GameObject> models)
    {
        arObjectModels = models;
        arModelDictionary = new Dictionary<string, EducationModel>();
        for (int i = 0; i < arObjectModels.Count; i++)
        {
            arModelDictionary.Add(i.ToString(),arObjectModels[i].AddComponent<EducationModel>());
        }
    }

    /// <summary>
    /// Sets the keys for the existing models in the dictionary
    /// </summary>
    /// <param name="keys"></param>
    public void SetKeys(string[] keys)
    {
        List<string> modelKeys = keys.ToList();
        SetKeys(modelKeys);

    }

    /// <summary>
    /// Sets the keys for the existing models in the dictionary
    /// </summary>
    /// <param name="keys"></param>
    public void SetKeys(List<string> keys)
    {
       
        Dictionary<string, EducationModel> tempDictionary = arModelDictionary;
        arModelDictionary = new Dictionary<string, EducationModel>();
        List<EducationModel> models = (from keyValuePair in tempDictionary where keyValuePair.Value != null select keyValuePair.Value).ToList();
        for (int i = 0; i < arObjectKeys.Count; i++)
        {
            if (models[i] != null)
            {
                arModelDictionary.Add(keys[i],models[i]);
                arObjectKeys.Add(keys[i]);
            }
        }
       
    }

    /// <summary>
    /// sets the model dictionary
    /// </summary>
    /// <param name="modelDictionary"></param>
    public void SetArModelDictionary(Dictionary<string, EducationModel> modelDictionary)
    {
        arModelDictionary = modelDictionary;
    }

    /// <summary>
    /// sets the model dictionary
    /// </summary>
    /// <param name="modelDictionary"></param>
    public void SetArModelDictionary(Dictionary<string, GameObject> modelDictionary)
    {
        arModelDictionary = new Dictionary<string, EducationModel>();
        foreach (KeyValuePair<string, GameObject> keyValuePair in modelDictionary)
        {
            arModelDictionary.Add(keyValuePair.Key,keyValuePair.Value.AddComponent<EducationModel>());
        }
      
    }
}
