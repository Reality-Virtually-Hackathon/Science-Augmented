using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollisionEvent : UnityEvent<Collision> {}

public class OnCollisionEvents : MonoBehaviour
{

    public CollisionEvent OnCollisionEnterEvent = new CollisionEvent();
    public CollisionEvent OnCollisionExitEvent = new CollisionEvent();
    public CollisionEvent OnCollisionStayEvent = new CollisionEvent();
    
    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterEvent.Invoke(collision);
    }

    // OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider
    private void OnCollisionExit(Collision collision)
    {
        OnCollisionExitEvent.Invoke(collision);
    }

    // OnCollisionStay is called once per frame for every collider/rigidbody that is touching rigidbody/collider
    private void OnCollisionStay(Collision collision)
    {
        OnCollisionStayEvent.Invoke(collision);
    }


}
