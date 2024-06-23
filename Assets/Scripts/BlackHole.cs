using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float gravityStrength = 5*9.81f;
    public int stack = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other){
        Rigidbody rb = other.attachedRigidbody;
        Planet pl = other.gameObject.GetComponent<Planet>();
        if(rb!=null&&pl!=null){
            Vector3 direction = (transform.position - other.transform.position).normalized;
            rb.AddForce(direction*gravityStrength*rb.mass);   
            if(Vector3.Distance(other.gameObject.transform.position,this.gameObject.transform.position)<1f +
                Shooter.Scale[pl.index]){
                stack += Shooter.logIndex[pl.index] * 2;
                Destroy(other.gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision other){
        Planet pl = other.gameObject.GetComponent<Planet>();
        if(pl!=null){
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other){

    }
}
