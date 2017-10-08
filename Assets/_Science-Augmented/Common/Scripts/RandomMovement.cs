using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {
    private float _endRange = 1;
    private float _startRange = 1.8f;
    private float _oscillateRange;
    private float _oscillateOffset;
    bool fit;
    Vector3 startPostion;

    void Start()
    {
        _oscillateRange = ( _endRange - _startRange ) / 4;
        _oscillateOffset = _oscillateRange + _startRange;
        startPostion = transform.localPosition;
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
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPostion, ( elapsedTime / time ));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void Update()
    {
        if(fit)
            return;
        
        float xpostion =  Mathf.Sin(Time.time) * _oscillateRange * Random.Range(0, 0.35f);
        float zpostion = Mathf.Sin(Time.time) * _oscillateRange * Random.Range(0, 0.35f);
        float ypostion =  Mathf.Sin(Time.time) * _oscillateRange * Random.Range(0, 0.35f);
        transform.localPosition = startPostion + new Vector3(xpostion,ypostion,zpostion);
    }
}
