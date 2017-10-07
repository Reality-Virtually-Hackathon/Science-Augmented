using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EducationModel : MonoBehaviour
{

    public string Key;
    public string CombinedValue;
    public string Value;
    public bool Enzyme = false;
    public GameObject Model;
    public void SetKey(string value)
    {
        Model = gameObject;
        Key = value;
    }
    public void SetValue(string value)
    {
        Model = gameObject;
        Value = value;
    }

    public void SetCombinedValue(string value)
    {
        CombinedValue = value;
    }

    public void SetModel(GameObject value)
    {
        Model = value;
    }

    public void Set(string value, GameObject model)
    {
        Key = value;
        Model = model;
    }
}
