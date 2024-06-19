using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public ObjectPool pool;
    public int index;
    public bool CollisionCheck=false;
    void Start(){
        pool=GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other) {
        Planet otherplanet = other.gameObject.GetComponent<Planet>();
        print(other.gameObject.name+this.gameObject.name);
        if(otherplanet!=null&&otherplanet.index == index){

            Vector3 position = (this.gameObject.transform.position + other.gameObject.transform.position)/2;
            int idx = gameObject.GetComponent<Planet>().index;
            pool.ReturnObject(other.gameObject,idx);
            pool.ReturnObject(this.gameObject,idx);
            if(!otherplanet.CollisionCheck&&!CollisionCheck){
                CollisionCheck=true;
                GameObject obj = pool.GetObject(idx+1);
                obj.transform.position = position;
            }
        }
    }
}
