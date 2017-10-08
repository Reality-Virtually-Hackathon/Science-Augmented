using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChildrenRotation : MonoBehaviour {
    public Vector3 RotationSpeeds = new Vector3(0,0,0);
    public Vector3 RotationSpeedMin = new Vector3(0, 0, 0);
    public Vector3 RotationSpeedsMax = new Vector3(0, 0, 0);
    bool fit;
    public float ChangeTime = 2f;
    // Use this for initialization
    void Start ()
    {
        StartRandomRotation();
    }

    public void SetInPlace(float time = 30)
    {
        fit = true;
        StopAllCoroutines();
        if(gameObject.activeSelf)
        StartCoroutine(StartSetInPlace(time));
    }

    public void StartRandomRotation()
    {
        fit = false;
        if (gameObject.activeSelf)
            StartCoroutine(ChangeRotation());
    }

    private IEnumerator StartSetInPlace( float time = 30)
    {
        fit = true;

        float elapsedTime = 0;
       

        while (elapsedTime < time)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localEulerAngles = Vector3.Lerp(transform.localEulerAngles, Vector3.zero, ( elapsedTime / time ));
            }
           
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator ChangeRotation()
    {
        RotationSpeeds = new Vector3(Random.Range(RotationSpeedMin.x, RotationSpeedsMax.x), Random.Range(RotationSpeedMin.y, RotationSpeedsMax.y), Random.Range(RotationSpeedMin.z, RotationSpeedsMax.z));

        yield return new WaitForSeconds(ChangeTime);
        StartCoroutine(ChangeRotation());
    }
    // Update is called once per frame
	void Update ()
	{
	    if(fit)
	    {
	        return;
	    }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Rotate(Vector3.up * ( RotationSpeeds.y * Time.deltaTime ));
            transform.GetChild(i).Rotate(Vector3.right * ( RotationSpeeds.x * Time.deltaTime ));
            transform.GetChild(i).Rotate(Vector3.forward * ( RotationSpeeds.z * Time.deltaTime ));
        }
      
    }
}
