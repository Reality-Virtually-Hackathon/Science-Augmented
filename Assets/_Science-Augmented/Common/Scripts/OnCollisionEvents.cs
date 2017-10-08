using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Enumerable = System.Linq.Enumerable;

[System.Serializable]
public class CollisionEvent : UnityEvent<List<EducationModel>> {}


public class OnCollisionEvents : MonoBehaviour
{

    public CollisionEvent OnCollisionEnterEvent = new CollisionEvent();
    public CollisionEvent OnCollisionExitEvent = new CollisionEvent();

    private  EducationModel thisEducationModel;
    private List<EducationModel> collisionObjects = new List<EducationModel>();
    Vuforia.ImageTargetBehaviour tracker;
    void Awake()
    {

         tracker = gameObject.transform.parent.GetComponent<Vuforia.ImageTargetBehaviour>();
        this.enabled = false;
        TurnOff();
    }

    void Update()
    {

        if (turnedOff && tracker.CurrentStatus == Vuforia.TrackableBehaviour.Status.DETECTED || tracker.CurrentStatus == Vuforia.TrackableBehaviour.Status.TRACKED)
        {
            Collider[] colider = GetComponentsInChildren<Collider>();
            for (int i = 0; i < colider.Length; i++)
            {
                
                colider[i].enabled = true;
            }
            turnedOff = false;
        }
        else
        {
            TurnOff();
        }
        
    }

    bool turnedOff;
    void TurnOff()
    {
        if(turnedOff)
            return;
       
            Collider[] colider = GetComponentsInChildren<Collider>();
            for (int i = 0; i < colider.Length; i++)
            {

                colider[i].enabled = false;
            }
        
        turnedOff = true;
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
      
        if(thisEducationModel == null)
        {
            thisEducationModel = GetComponentInChildren<EducationModel>();
        }

        EducationModel collisonModel = collision.gameObject.GetComponentInChildren<EducationModel>();
        print("Hit");
        if (collisonModel!= null)
        {
            print("Invoke");
           
            if(!collisionObjects.Contains(collisonModel))
            {
                collisionObjects.Add(collisonModel);
            }
            int combinedValue = thisEducationModel.Value;
            collisionObjects.Add(thisEducationModel);
            OnCollisionEnterEvent.Invoke(collisionObjects);
            
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
            
            OnCollisionExitEvent.Invoke(collisionObjects);
            if (collisionObjects.Contains(collisonModel))
            {
                collisionObjects.Remove(collisonModel);
                collisonModel.CombinedValue = thisEducationModel.CombinedValue = 0;
            }
        }
    }

 


}
