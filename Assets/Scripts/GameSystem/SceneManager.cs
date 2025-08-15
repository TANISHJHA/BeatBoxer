using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CSCore;

public class scene_switch : MonoBehaviour
{

    public void scene_changer(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    } 

    
}
