using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool onBag = false;
    private bool onZone = false;
    public float speed = 1.0f;
    public GameObject manager; 
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10.0f);
        manager = GameObject.Find("GameManager");
        gameManager = manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime; 

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
}
