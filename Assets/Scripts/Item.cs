
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public Image gauge;
    // Start is called before the first frame update
    void Start()
    {
        gauge.fillAmount=0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        gauge.fillAmount=(float)User.GetItemGauge() /(float) User.maxItemCut;
        if(gauge.fillAmount==1f){
            gauge.color=Color.red;
        }else{
            gauge.color=Color.white;
        }
    }
}
