using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EducationModel : MonoBehaviour
{

    public int Key;
    public int CombinedValue;
    public int Value;
    public bool Enzyme = false;
    public GameObject Model;
    public void SetKey(int value)
    {
        Model = gameObject;
        Key = value;
    }
    public void SetValue(int value)
    {
        Model = gameObject;
        Value = value;
    }

    public void SetCombinedValue(int value)
    {
        CombinedValue = value;
    }

    public void SetModel(GameObject value)
    {
        Model = value;
    }

    public void Set(int value, GameObject model)
    {
        Key = value;
        Model = model;
    }
}
