using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Miss : MonoBehaviour
{
    public GameObject manager;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        gameManager = manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bag"))
        {
            //Destroy(this.gameObject); 
            //hitEffect.SetActive(false); 
            //hitEffect.SetActive(true); 
            gameManager.decreaseHP();

        }
    }
}
