using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
public class User : MonoBehaviour
{
    public static User UserInstance;
    public GameObject Camera;
    public Vector3 Target;
    public float distance;
    public float speed = 10.0f;
    float alpha = 0.0f;
    float beta = 0.0f;
    float x,y,z;

    GameObject Launcher;
    GameObject Showable;
    bool shootlock=false;

    public static int score = 0;
    public static int maxidx=0; 
    public static bool GameOver = false;
    public static int itemgauge = 0;
    public static int maxItemCut=50;
    // Start is called before the first frame update
    void Start()
    {
        UserInstance=this;
        Target = new Vector3(0,0,0);
        distance = 10;
        transform.position = new Vector3(distance,0,0);
        Camera = transform.GetChild(0).gameObject;
        x=0;y=0;z=0;
        Showable=Shooter.GetObject(0);
        //Destroy(Showable.GetComponent<Rigidbody>());
    }
    public static void Renew_maxidx(int idx){
        if(maxidx<idx){
            maxidx=idx;
        }
    }
    public static void AddScore(int scr){
        score+=scr*(scr + 1)/2;
    }
    public static int GetItemGauge(){
        return itemgauge;
    }
    public static void AddItemGauge(int cnt){
        if(itemgauge + cnt >= maxItemCut){
            itemgauge = maxItemCut;
        }else{
            itemgauge += cnt;
        }
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
        if(!GameOver){
            CameraRotate();
            CameraMove();
            //Showable.transform.position = distance*new Vector3(x,y,z);
            if(Input.GetKeyDown(KeyCode.Space)){
                if(!shootlock)
                    ShootPlanet();
            
            }
        }
    }
    async void ShootPlanet(){
        shootlock=true;
        Launcher = Shooter.GetObject((int)Random.Range(0,maxidx/2));
        Launcher.transform.position=distance*new Vector3(x,y,z);
        Launcher.GetComponent<Rigidbody>().AddForce(60*new Vector3(-x,-y,-z));
        await Task.Delay(1500);
        shootlock=false;
    }
}
