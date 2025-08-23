using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IDataPersistance
{
    private int score = 0;
    private int highScore = 0;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        //addScore();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
