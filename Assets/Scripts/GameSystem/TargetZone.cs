using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZone : MonoBehaviour
{
    private bool targetAva = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public bool hasTarget()
    {
        return targetAva;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Target"))
        {
            //Destroy(this.gameObject); 
            targetAva = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            targetAva = false;
        }
    }
}
