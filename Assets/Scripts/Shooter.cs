using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooter : MonoBehaviour
{
    public static Shooter ShooterInstance;
    public int PlanetCnt=8;
    public GameObject[] Planets;
    public string[] Order = {"Mercury", "Mars", "Venus","Earth","Neptune","Uranus","Saturn","Jupiter"};
    public static float[] Scale = {1.0f, 1.39f,2.48f,2.61f,10.09f,10.39f,23.87f,28.66f };
    // Start is called before the first frame update
    void Awake()
    {
        ShooterInstance = this;
        Planets = new GameObject[PlanetCnt];
        for(int i=0;i<PlanetCnt;i++){
            GameObject Prefab = Resources.Load<GameObject>("Planets/"+Order[i]);
            Planets[i]=Prefab;
        }
    }

    // Update is called once per frame
    public static GameObject GetObject(int index)
    {
        GameObject obj = Instantiate(ShooterInstance.Planets[index]);
        obj.transform.localScale = new Vector3(Scale[index],Scale[index],Scale[index]);
        obj.AddComponent<Planet>();
        obj.GetComponent<Planet>().index=index;
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().useGravity=false;
        obj.GetComponent<Rigidbody>().velocity=Vector3.zero;
        return obj;
    }
}
