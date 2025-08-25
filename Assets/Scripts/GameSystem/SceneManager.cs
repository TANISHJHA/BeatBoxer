using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CSCore;
using CSCore.XAudio2;

public class scene_switch : MonoBehaviour
{
    public string easy = "BeatBoxer";
    public string medium = "BeatBoxerMedium";
    public string hard = "BeatBoxerHard";
    public string extreme = "BeatBoxerExtreme";
    private string nextScene = "BeatBoxer";

    public void changeScene(int mode)
    {
        switch(mode)
        {
            case 0:
                nextScene = easy;
                break;
            case 1:
                nextScene = medium;
                break; 
            case 2: 
                nextScene = hard;
                break;
            case 3:
                nextScene = extreme;
                break;
            case 4:
                nextScene = "StartScreen";
                break;
        }
    }
    public void scene_changer()
    {
        SceneManager.LoadScene(nextScene);
    } 

    
}
