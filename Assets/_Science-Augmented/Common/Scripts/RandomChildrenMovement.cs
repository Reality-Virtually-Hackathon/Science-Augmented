using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChildrenMovement : MonoBehaviour {
    private float _endRange = 1;
    private float _startRange = 1.8f;
    private float _oscillateRange;
    private float _oscillateOffset;
    bool fit;
    List<Vector3> startPostions = new List<Vector3>();

    void Start()
    {
        _oscillateRange = ( _endRange - _startRange ) / 10;
        _oscillateOffset = _oscillateRange + _startRange;
        for (int i = 0; i < transform.childCount; i++)
        {
            startPostions.Add(transform.GetChild(i).localPosition);
        }
       
    }

    public void SetRandom()
    {
        fit = false;
    }

    public void SetBack()
    {
        fit = true;
        StartCoroutine(StartSetInPlace());
    }

    private IEnumerator StartSetInPlace(float time = 2)
    {
        fit = true;

        float elapsedTime = 0;


        while (elapsedTime < time)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localPosition = Vector3.Lerp(transform.localPosition, startPostions[i], ( elapsedTime / time ));
            }
         
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void Update()
    {
        if(fit)
            return;
        for (int i = 0; i < transform.childCount; i++)
        {
            float xpostion =  Mathf.Sin(Time.time * Random.Range(0, 1f)) * _oscillateRange ;
            float zpostion = Mathf.Sin(Time.time * Random.Range(0, 17f)) * _oscillateRange ;
            float ypostion = Mathf.Sin(Time.time * Random.Range(0, 1f)) * _oscillateRange;
        
            transform.GetChild(i).localPosition = startPostions[i] + new Vector3(xpostion, ypostion, zpostion);
        }
      
    }
}
