using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor;
using UnityEngine;
public class ObjectPool : MonoBehaviour
{
    public int PlanetCnt=8;
    public GameObject[] Planets;
    public string[] Order = {"Mercury", "Mars", "Venus","Earth","Neptune","Uranus","Saturn","Jupiter"};
    public int poolsize = 10;
    public Queue<GameObject>[] pool;
    // Start is called before the first frame update
    void Awake(){
        Planets = new GameObject[PlanetCnt];
        pool = new Queue<GameObject>[PlanetCnt];
        for(int i=0;i<PlanetCnt;i++){
            GameObject Prefab = Resources.Load<GameObject>("Planets/"+Order[i]);
            Planets[i]=Prefab;
            pool[i]=new Queue<GameObject>();
            for(int j=0;j<poolsize;j++){
                GameObject obj = Instantiate(Prefab);
                obj.transform.SetParent(this.transform);
                obj.SetActive(false);
                obj.transform.position=new Vector3(11111,11111,1111);
                obj.AddComponent<Planet>();
                obj.AddComponent<Rigidbody>();
                obj.GetComponent<Rigidbody>().useGravity=false;
                pool[i].Enqueue(obj);
            }
        }
    }
    public GameObject GetObject(int index){
        GameObject obj;
        if(pool[index].Count > 0){
            obj = pool[index].Dequeue();
            obj.SetActive(true);
        }
        else{
            obj = Instantiate(Planets[index]);   
        }
        obj.GetComponent<Planet>().index=index;
        obj.GetComponent<Rigidbody>().velocity=Vector3.zero;
        return obj;
    }
    public void ReturnObject(GameObject obj,int index){
        obj.SetActive(false);
        pool[index].Enqueue(obj);
    }
    // Update is called once per frame
}
