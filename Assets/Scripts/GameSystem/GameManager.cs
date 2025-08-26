using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CSCore.XAudio2;
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
        highScoreText.SetText("High Score: " + data.highScore);
    }

    public void SaveData(ref GameData data)
    {
        data.highScore = this.highScore;
    } 

    public void updateTotalTargets()
    {
        totalTargets++;
    }

    public void addScore()
    {
        hitCombo++;
        hitTargets++;
        accuracyText.SetText("Acc: " + ((hitTargets/totalTargets) * 100).ToString("0.0") + "%");
        comboText.SetText(hitCombo.ToString() + "x");
        UpdateText(score + 100, score, scoreText);
        score += 100; 
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
