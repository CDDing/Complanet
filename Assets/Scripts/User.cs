
using System.Threading.Tasks;
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
    bool shootlock=false;

    public static int score = 0;
    public static int maxidx=0; 
    public static bool GameOver = false;
    public static int itemgauge = 0;
    public static int maxItemCut=50;
    public GameObject Panel;
    
    // Start is called before the first frame update
    void Start()
    {
        UserInstance=this;
        Target = new Vector3(0,0,0);
        distance = 20;
        transform.position = new Vector3(distance,0,0);
        Camera = transform.GetChild(0).gameObject;
        x=0;y=0;z=0;
        Launcher = Shooter.GetObject((int)Random.Range(0,maxidx/2));
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
    public static int GetScore(){
        return score;
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
            
            if(!shootlock&&Launcher!=null){
                Launcher.transform.position=distance*new Vector3(x,y,z);
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                if(!shootlock)
                    ShootPlanet();
            
            }
            if(Input.GetMouseButtonDown(0)){
                if(itemgauge==maxItemCut){
                    itemgauge=0;
                    ShootItem();
                }

            }
        }
        if(GameOver){
            Panel.SetActive(true);
        }
    }
    async void ShootPlanet(){
        shootlock=true;
        
        if(Launcher!=null){
            Launcher.AddComponent<Rigidbody>();
            Launcher.GetComponent<Rigidbody>().useGravity=false;
            Launcher.GetComponent<Rigidbody>().velocity=Vector3.zero;
            Launcher.GetComponent<Rigidbody>().AddForce(80*new Vector3(-x,-y,-z));
            await Task.Delay(1500);
        }
        Launcher = Shooter.GetObject((int)Random.Range(0,maxidx/2));
        shootlock=false;
    }
    async public void ShootItem(){
        Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);


        Plane plane = new Plane(new Vector3(x,y,z),distance *new Vector3(x,y,z));
        float rayDistance;
        GameObject obj = Shooter.GetItem();
        if(plane.Raycast(ray,out rayDistance)){
            Vector3 hitPoint = ray.GetPoint(rayDistance);
            obj.transform.position = hitPoint;
            obj.GetComponent<Rigidbody>().AddForce(100*new Vector3(-x,-y,-z));
        }
        await Task.Delay(10000);
        BlackHole BH = obj.GetComponent<BlackHole>();
        int index = (int) Mathf.Log(BH.stack,2f);
        if(index >1){
            if(index>=8){
                index = 7;
            }
            GameObject newPlanet = Shooter.GetObject(index);
            
            newPlanet.AddComponent<Rigidbody>();
            newPlanet.GetComponent<Rigidbody>().useGravity=false;
            newPlanet.GetComponent<Rigidbody>().velocity=Vector3.zero;
            newPlanet.transform.position=obj.transform.position;
        }

        Destroy(obj);
    }
}
