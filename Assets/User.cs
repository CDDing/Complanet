using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public Vector3 Target;
    public float distance;
    public float speed = 10.0f;
    float alpha = 0.0f;
    float beta = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Target = new Vector3(0,0,0);
        distance = 10;
        transform.position = new Vector3(distance,0,0);
    }

    void CameraMove(){
        float LeftRightInput = Input.GetAxis("Horizontal");
        float UpDownInput = Input.GetAxis("Vertical");
        alpha += LeftRightInput*Time.deltaTime;
        beta += UpDownInput*Time.deltaTime;

        float x = Mathf.Cos(beta)* Mathf.Cos(alpha);
        float y = Mathf.Sin(beta);
        float z = Mathf.Cos(beta) * Mathf.Sin(alpha);
        transform.position = distance* new Vector3(x,y,z);

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
    }
}
