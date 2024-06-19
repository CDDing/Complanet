using System.Collections;
using UnityEngine;

public class User : MonoBehaviour
{
    public ObjectPool pool;
    public GameObject Camera;
    public Vector3 Target;
    public float distance;
    public float speed = 10.0f;
    float alpha = 0.0f;
    float beta = 0.0f;
    float x,y,z;

    GameObject Launcher;
    GameObject Showable;
    // Start is called before the first frame update
    void Start()
    {
        pool=GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        Target = new Vector3(0,0,0);
        distance = 10;
        transform.position = new Vector3(distance,0,0);
        Camera = transform.GetChild(0).gameObject;
        x=0;y=0;z=0;
        Showable=pool.GetObject(0);
        Destroy(Showable.GetComponent<Rigidbody>());
    }

    void CameraMove(){
        float LeftRightInput = Input.GetAxis("Horizontal");
        float UpDownInput = Input.GetAxis("Vertical");
        alpha += LeftRightInput*Time.deltaTime;
        beta += UpDownInput*Time.deltaTime;

        x = Mathf.Cos(beta)* Mathf.Cos(alpha);
        y = Mathf.Sin(beta);
        z = Mathf.Cos(beta) * Mathf.Sin(alpha);
        transform.position = distance* new Vector3(x,y,z);

        float camx = Mathf.Cos(beta + 0.08f)* Mathf.Cos(alpha);
        float camy = Mathf.Sin(beta + 0.08f);
        float camz = Mathf.Cos(beta + 0.08f) * Mathf.Sin(alpha);
        Camera.transform.position = (distance + 5) * new Vector3(camx,camy,camz);
    }

    void CameraRotate(){
        Vector3 vec = Target - transform.position;
        transform.rotation = Quaternion.LookRotation(vec).normalized;
        
    }
    // Update is called once per frame
    void Update()
    {
        CameraRotate();
        CameraMove();
        Showable.transform.position = distance*new Vector3(x,y,z);
        if(Input.GetKeyDown(KeyCode.Space)){
            ShootPlanet();
        }
    }
    void ShootPlanet(){
        Launcher = pool.GetObject(0);
        Launcher.transform.position=distance*new Vector3(x,y,z);
        Launcher.GetComponent<Rigidbody>().AddForce(60*new Vector3(-x,-y,-z));

    }
}
