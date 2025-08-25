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

    public void changePosition(int button)
    {

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
}
