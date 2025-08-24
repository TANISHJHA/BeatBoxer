using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistance
{
    private int score = 0;
    private int highScore = 0;
    private int hp = 100;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText;
    public scene_switch sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        //addScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            //Trigger Game over 
            sceneManager.scene_changer("StartScreen");
        }
    } 
    

    
    public void LoadData(GameData data)
    {
        this.highScore = data.highScore;
    }

    public void SaveData(ref GameData data)
    {
        data.highScore = this.highScore;
    }

    public void addScore()
    {
        score += 100; 
        if (score > highScore)
        {
            highScore = score;
        }
        scoreText.text = score.ToString();
    } 

    public void decreaseHP()
    {
        hp -= 10; 
        hpText.text = hp.ToString();
    }
}
