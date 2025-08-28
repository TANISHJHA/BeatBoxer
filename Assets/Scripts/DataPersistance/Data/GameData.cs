using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int highScore;
    public int easyHighScore;
    public int mediumHighScore;
    public int hardHighScore;
    public int extremeHighScore;
    public float easyAcc; 
    public float mediumAcc;
    public float hardAcc;
    public float extremeAcc;
    public GameData()
    {
        highScore = 0; 
        easyHighScore = 0;
        mediumHighScore = 0;
        hardHighScore = 0;
        extremeHighScore = 0;
        easyAcc = 0;
        mediumAcc = 0;
        hardAcc = 0;    
        extremeAcc = 0;
    }
}
