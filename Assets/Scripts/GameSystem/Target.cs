using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool onBag = false;
    private bool onZone = false;
    public float speed = 1.0f;
    public float difficulty = 1.0f;
    public GameObject manager;
    public GameObject hitEffect;
    private GameManager gameManager; 


    // Start is called before the first frame update
    void Start()
    {
        Invoke("miss", difficulty);
        manager = GameObject.Find("GameManager");
        gameManager = manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isPaused)
        {
            Destroy(this.gameObject);
        }
        //transform.position += Vector3.down * speed * Time.deltaTime; 
        transform.localScale = transform.localScale * 1.001f;
        if (onBag && onZone)
        {
            Destroy(this.gameObject);
            gameManager.addScore();
        }
    } 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bag"))
        {
            //Destroy(this.gameObject); 
            //hitEffect.SetActive(false); 
            //hitEffect.SetActive(true); 
            GameObject hit = Instantiate(hitEffect, transform.position, transform.rotation);
            onBag = true; 
            
        }

        if (other.gameObject.CompareTag("Zone"))
        {
            //Destroy(this.gameObject); 
            onZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bag"))
        {
            //Destroy(this.gameObject); 
            onBag = false;
        }

        if (other.gameObject.CompareTag("Zone"))
        {
            //Destroy(this.gameObject); 
            onZone = false;
        }
    } 

    private void miss()
    {
        gameManager.missUpdate();
        Destroy(this.gameObject);
    }
}
