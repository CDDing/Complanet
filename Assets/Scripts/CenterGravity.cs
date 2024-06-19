using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float gravityStrength = 9.81f;
    // Start is called before the first frame update
    void OnTriggerStay(Collider other){
        Rigidbody rb = other.attachedRigidbody;
        if(rb!=null){
            Vector3 direction = (transform.position - other.transform.position).normalized;
            rb.AddForce(direction*gravityStrength*rb.mass);
        }
    }
}
