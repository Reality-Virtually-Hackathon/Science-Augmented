using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Enumerable = System.Linq.Enumerable;

[System.Serializable]
public class CollisionEvent : UnityEvent<EducationModel, EducationModel> {}
[System.Serializable]
public class MultiCollisionEvent : UnityEvent<EducationModel, EducationModel, EducationModel> { }

public class OnCollisionEvents : MonoBehaviour
{

    public CollisionEvent OnCollisionEnterEvent = new CollisionEvent();
    public CollisionEvent OnCollisionExitEvent = new CollisionEvent();
    public CollisionEvent OnCollisionStayEvent = new CollisionEvent();
    private  EducationModel thisEducationModel;
    private List<EducationModel> collisionObjects = new List<EducationModel>();
    void Awake()
    {
        
     
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        print("Hit");
        if(thisEducationModel == null)
        {
            thisEducationModel = GetComponentInChildren<EducationModel>();
        }

        EducationModel collisonModel = collision.gameObject.GetComponentInChildren<EducationModel>();
        if(collisonModel!= null)
        {
            OnCollisionEnterEvent.Invoke(thisEducationModel,collisonModel);
            if(!collisionObjects.Contains(collisonModel))
            {
                collisionObjects.Add(collisonModel);
            }
            string combinedValue = thisEducationModel.Value;
            print(combinedValue);
            EducationModel enzyme = collisionObjects.Find(e => e.Enzyme);
            if(enzyme == false)
            {
                return;
            }

            combinedValue = Enumerable.Aggregate(collisionObjects, combinedValue, (current, t) => current + t.Value);
            thisEducationModel.CombinedValue = combinedValue;
            for (int i = 0; i < collisionObjects.Count; i++)
            {
                collisionObjects[i].CombinedValue = combinedValue;
            }
        }
       
    }

    // OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider
    private void OnCollisionExit(Collision collision)
    {
        if (thisEducationModel == null)
        {
            thisEducationModel = GetComponentInChildren<EducationModel>();
        }
        EducationModel collisonModel = collision.gameObject.GetComponentInChildren<EducationModel>();
        if (collisonModel != null)
        {
            
            OnCollisionExitEvent.Invoke(thisEducationModel,collisonModel);
            if (collisionObjects.Contains(collisonModel))
            {
                collisionObjects.Remove(collisonModel);
                collisonModel.CombinedValue = thisEducationModel.CombinedValue = "";
            }
        }
    }

    // OnCollisionStay is called once per frame for every collider/rigidbody that is touching rigidbody/collider
    private void OnCollisionStay(Collision collision)
    {
        if (thisEducationModel == null)
        {
            thisEducationModel = GetComponentInChildren<EducationModel>();
        }
        EducationModel collisonModel = collision.gameObject.GetComponentInChildren<EducationModel>();
        if (collisonModel != null)
        {
            OnCollisionStayEvent.Invoke(thisEducationModel,collisonModel);
        }
    }


}
