using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    public Text text;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(newGame);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Game Over!! \n Score : "+User.GetScore();
    }
    void newGame(){
        SceneManager.LoadScene(1);
    }
}
