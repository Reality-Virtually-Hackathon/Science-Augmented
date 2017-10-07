using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollisionEvent : UnityEvent<EducationModel, EducationModel> {}

public class OnCollisionEvents : MonoBehaviour
{

    public CollisionEvent OnCollisionEnterEvent = new CollisionEvent();
    public CollisionEvent OnCollisionExitEvent = new CollisionEvent();
    public CollisionEvent OnCollisionStayEvent = new CollisionEvent();
    EducationModel thisEducationModel;

    void Awake()
    {
        thisEducationModel = GetComponentInChildren<EducationModel>();
        if(thisEducationModel== null)
            gameObject.SetActive(false);
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        EducationModel collisonModel = collision.gameObject.GetComponentInChildren<EducationModel>();
        if(collisonModel!= null)
        {
            OnCollisionEnterEvent.Invoke(thisEducationModel,collisonModel);
        }
    }

    // OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider
    private void OnCollisionExit(Collision collision)
    {
        EducationModel collisonModel = collision.gameObject.GetComponentInChildren<EducationModel>();
        if (collisonModel != null)
        {
            OnCollisionExitEvent.Invoke(thisEducationModel,collisonModel);
        }
    }

    // OnCollisionStay is called once per frame for every collider/rigidbody that is touching rigidbody/collider
    private void OnCollisionStay(Collision collision)
    {
        EducationModel collisonModel = collision.gameObject.GetComponentInChildren<EducationModel>();
        if (collisonModel != null)
        {
            OnCollisionStayEvent.Invoke(thisEducationModel,collisonModel);
        }
    }


}
