using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public GameObject easyButton;
    public GameObject mediumButton;
    public GameObject hardButton;
    public GameObject extremeButton;
    public int currentDifficulty = 0;

    public void changePosition(int button)
    {
        currentDifficulty = button;
        switch (button)
        {
            case 0:
                this.transform.position = easyButton.transform.position;
                break;
            case 1:
                this.transform.position = mediumButton.transform.position;
                break;
            case 2:
                this.transform.position = hardButton.transform.position;
                break;
            case 3:
                this.transform.position = extremeButton.transform.position;
                break;
        }
    }

    public void move(int upDown)
    {
        if (upDown == 0)
        {
            currentDifficulty--;
            if (currentDifficulty <= 0)
            {
                currentDifficulty = 0;
            } 
            changePosition(currentDifficulty);
        } 
        else if (upDown == 1)
        {
            currentDifficulty++;
            if (currentDifficulty >= 3)
            {
                currentDifficulty = 3;
            } 
            changePosition(currentDifficulty);
        }
    }
}
