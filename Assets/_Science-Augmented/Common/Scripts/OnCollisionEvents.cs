using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Enumerable = System.Linq.Enumerable;

[System.Serializable]
public class CollisionEvent : UnityEvent<List<ModelGroup>> {}


public class OnCollisionEvents : MonoBehaviour
{

    public CollisionEvent OnCollisionEnterEvent = new CollisionEvent();
    public CollisionEvent OnCollisionExitEvent = new CollisionEvent();

    private ModelGroup thisEducationModel;
    private List<ModelGroup> collisionObjects = new List<ModelGroup>();
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
   
        
            thisEducationModel = GetComponentInChildren<ModelGroup>(true);
        

        ModelGroup collisonModel = collision.gameObject.GetComponentInChildren<ModelGroup>();
    
        if (collisonModel!= null)
        {
          
           
            if(!collisionObjects.Contains(collisonModel))
            {
                collisionObjects.Add(collisonModel);
            }
            for (int i = 0; i < collisionObjects.Count; i++)
            {
                collisionObjects[i].transform.localPosition = collisionObjects[i].StartLocal;
            }
            int combinedValue = thisEducationModel.ActivatedActiveModel.Value;
            collisionObjects.Add(thisEducationModel);
            OnCollisionEnterEvent.Invoke(collisionObjects);

         
            

            combinedValue = Enumerable.Aggregate(collisionObjects, combinedValue, (current, t) => current + t.ActivatedActiveModel.Value);
            thisEducationModel.ActivatedActiveModel.CombinedValue = combinedValue;
            
            for (int i = 0; i < collisionObjects.Count; i++)
            {
                collisionObjects[i].ActivatedActiveModel.CombinedValue = combinedValue;
            }
        }
       
    }

    // OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider
    private void OnCollisionExit(Collision collision)
    {
        
            thisEducationModel = GetComponentInChildren<ModelGroup>(true);
        
        ModelGroup collisonModel = collision.gameObject.GetComponentInChildren<ModelGroup>();
        OnCollisionExitEvent.Invoke(collisionObjects);
        if (collisonModel != null)
        {
            
          
            if (collisionObjects.Contains(collisonModel))
            {
                collisionObjects.Remove(collisonModel);
                collisonModel.ActivatedActiveModel.CombinedValue = thisEducationModel.ActivatedActiveModel.CombinedValue = 0;
            }
        }
    }

 


}
