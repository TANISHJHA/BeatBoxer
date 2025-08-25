using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scene_switch switchManager = FindObjectOfType<scene_switch>();
            switchManager.scene_changer();
        } 


        
    } 



}
