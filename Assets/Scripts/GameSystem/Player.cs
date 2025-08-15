using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<scene_switch>().scene_changer("BeatBoxer");
        }
        
    }

}
