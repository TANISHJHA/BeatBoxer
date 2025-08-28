using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CSCore.XAudio2;
using JetBrains.Annotations;
[RequireComponent(typeof(TextMeshProUGUI))]
public class GameManager : MonoBehaviour, IDataPersistance
{
    private int score = 0;
    private int highScore = 0;
    private int hp = 100;
    private float missedTargets = 0;
    private int hitCombo = 0;
    private float hitTargets = 0;
    private float totalTargets = 0;
    private int easyHighScore = 0;
    private int mediumHighScore = 0;
    private int hardHighScore = 0;
    private int extremeHighScore = 0;
    private float easyAcc = 0;
    private float mediumAcc = 0;
    private float hardAcc = 0;
    private float extremeAcc = 0;
    private float currentAcc = 0;
    private bool easyBeaten = false;
    private bool mediumBeaten = false;
    private bool hardBeaten = false;
    private bool extremeBeaten = false;
    public bool isPaused = false;
    public int CountFPS = 30;
    public int Duration = 1;
    public string NumberFormat = "N0";
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText; 
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI accuracyText;
    public GameObject pauseMenu;
    public DataPersistanceManager dataPersistanceManager;
    public Selector selector;
    public int difficulty;
    public scene_switch sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        //addScore(); 
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            //Trigger Game over  
            if (easyBeaten)
            {
                easyAcc = this.currentAcc;
            }
            if (mediumBeaten)
            {
                mediumAcc = this.currentAcc;
            } 
            if (hardBeaten)
            {
                hardAcc = this.currentAcc;
            } 
            if (extremeBeaten)
            {
                extremeAcc = this.currentAcc;
            }
            dataPersistanceManager.SaveGame();
            sceneManager.changeScene(4);
            sceneManager.scene_changer();
        } 

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pause();
            } 
            else
            {
                unPause();
            }
        } 

        if (Input.GetKeyDown(KeyCode.F1))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        if (selector != null)
        {
            difficulty = selector.currentDifficulty;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selector != null)
            {
                selector.move(0);
                difficulty = selector.currentDifficulty;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selector != null)
            {
                selector.move(1);
                difficulty = selector.currentDifficulty;
            }
        }



        updateHighScoreText();
    }

    private void updateHighScoreText()
    {
        switch (difficulty)
        {
            case 0:
                highScoreText.SetText("High Score: " + easyHighScore + "\n       Acc: " + easyAcc.ToString("0.0") + "%");
                break;
            case 1:
                highScoreText.SetText("High Score: " + mediumHighScore + "\n       Acc: " + mediumAcc.ToString("0.0") + "%");
                break;
            case 2:
                highScoreText.SetText("High Score: " + hardHighScore + "\n       Acc: " + hardAcc.ToString("0.0") + "%");
                break;
            case 3:
                highScoreText.SetText("High Score: " + extremeHighScore + "\n       Acc: " + extremeAcc.ToString("0.0") + "%");
                break;
        }
    }

    public void pause()
    { 
        
        Time.timeScale = 0.0f;
        isPaused = true; 
        pauseMenu.SetActive(true);
    } 
    
    public void unPause()
    {
        
        Time.timeScale = 1.0f;
        isPaused = false; 
        pauseMenu.SetActive(false);
    }

    public void closeApplication()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
    public void LoadData(GameData data)
    { 
        
        this.highScore = data.highScore; 
        this.easyHighScore = data.easyHighScore;
        this.mediumHighScore = data.mediumHighScore;
        this.hardHighScore = data.hardHighScore;
        this.extremeHighScore = data.extremeHighScore;
        this.easyAcc = data.easyAcc;
        this.mediumAcc = data.mediumAcc;
        this.hardAcc = data.hardAcc;
        this.extremeAcc = data.extremeAcc;
        highScoreText.SetText("High Score: " + data.highScore);
    }

    public void SaveData(ref GameData data)
    {
        data.highScore = this.highScore; 
        data.easyHighScore = this.easyHighScore;
        data.mediumHighScore = this.mediumHighScore; 
        data.hardHighScore = this.hardHighScore; 
        data.extremeHighScore = this.extremeHighScore; 
        data.easyAcc = this.easyAcc;
        data.mediumAcc = this.mediumAcc;
        data.hardAcc = this.hardAcc;
        data.extremeAcc= this.extremeAcc;
    } 

    public void updateTotalTargets()
    {
        totalTargets++;
    }

    public void addScore()
    {
        hitCombo++;
        hitTargets++;
        currentAcc = (hitTargets / totalTargets) * 100;
        accuracyText.SetText("Acc: " + ((hitTargets/totalTargets) * 100).ToString("0.0") + "%");
        comboText.SetText(hitCombo.ToString() + "x");
        UpdateText(score + 100, score, scoreText);
        score += 100;
        switch (difficulty)
        {
            case 0:
                if(score > easyHighScore)
                {
                    easyHighScore = score;
                    easyBeaten = true;
                }
                break;
            case 1:
                if (score > mediumHighScore)
                {
                    mediumHighScore = score; 
                    mediumBeaten = true;
                }
                break;
            case 2:
                if (score > hardHighScore)
                {
                    hardHighScore = score;
                    hardBeaten = true;
                }
                break;
            case 3:
                if (score > extremeHighScore)
                {
                    extremeHighScore = score; 
                    extremeBeaten = true;
                }
                break;
        }
        if (score > highScore)
        {
            highScore = score;
        }
        //scoreText.text = score.ToString();
    } 

    public void decreaseHP()
    {
        missUpdate();
        UpdateText(hp-10,hp,hpText);
        hp -= 10;
    } 

    public void missUpdate()
    {
        missedTargets++;
        currentAcc = (hitTargets / totalTargets)*100;
        accuracyText.SetText("Acc: " + ((hitTargets/totalTargets)*100).ToString("0.0") + "%");
        comboText.SetText("");
        hitCombo = 0;
    }

    private Coroutine CountingCoroutine;

    private void UpdateText(int newValue, int prevValue, TextMeshProUGUI text)
    {
        if (CountingCoroutine != null)
        {
            StopCoroutine(CountingCoroutine);
        }

        CountingCoroutine = StartCoroutine(CountText(newValue, prevValue, text));
    }

    private IEnumerator CountText(int newValue, int prevValue, TextMeshProUGUI text)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f/CountFPS);
        int previousValue = prevValue;
        int stepAmount;

        if (newValue - previousValue < 0)
        {
            stepAmount = -1; 
        }
        else
        {
            stepAmount = 10; 
        }
        

        if (previousValue < newValue)
        {
            while (previousValue < newValue)
            {
                previousValue += stepAmount;
                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }

                text.SetText(previousValue.ToString(NumberFormat));

                yield return Wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount; // (-20 - 0) / (30 * 1) = -0.66667 -> -1              0 + -1 = -1
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }

                text.SetText(previousValue.ToString(NumberFormat));

                yield return Wait;
            }
        }
    }
}
