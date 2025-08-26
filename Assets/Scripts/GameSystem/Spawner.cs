using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject target;
    public GameObject targetZone;
    public GameManager gameManager;
    public TargetZone[] zones;
    public SpeedBag speedBag;
    public Vector3 offset;
    public int numTargets = 100;
    public float easyDifficulty = 1.0f;
    public float mediumDifficulty = 0.8f;
    public float hardDifficulty = 0.5f;
    public float extremeDifficulty = 0.4f;
    public float difficulty;
    public int setDifficulty;
    private int currentSpawn = 0;
    private int spawnSide;
    public List<float> spawnTimings = new List<float>(); //Array holding how many seconds to wait before spawning next target. 
    public List<int> sides = new List<int>(); //Which side to spawn the target 0 = left, 1 = right   
    private bool stopSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        if (speedBag.isRecording())
        {
            stopSpawn = true;
        } 

        switch(setDifficulty)
        {
            case 0:
                difficulty = easyDifficulty;
                break; 
            case 1:
                difficulty = mediumDifficulty;
                break;
            case 2:
                difficulty = hardDifficulty;
                break;
            case 3:
                difficulty = extremeDifficulty;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (speedBag.isRecording())
        {
            stopSpawn = true; 
        } 
        
    } 

    void OnEnable()
    {
        if (!stopSpawn)
        {
            //Spawn(); 
            //beatSpawn();
        }
    } 

    /// <summary>
    /// Recursive spawn function to spawn targets from memory. 
    /// </summary>
    void Spawn()
    {
        if (stopSpawn)
        {
            return;
        }
        Vector3 pos = transform.position;
        if (sides[currentSpawn] == 1)
        {
            pos += offset;
        }
        //GameObject brick = Instantiate(target, pos, transform.rotation);
        currentSpawn++; 
        if (currentSpawn < numTargets)
        {
            Invoke(nameof(Spawn), spawnTimings[currentSpawn]);
        }
    } 

    public void beatSpawn()
    {
        if (gameManager.isPaused)
        {
            return;
        }
        Vector3 pos = targetZone.transform.position;
        if (spawnSide == 0)
        {
            spawnSide = 1;
        }
        else
        {
            spawnSide = 0;
            pos += offset;
        } 
        if (!zones[spawnSide].hasTarget())
        {
            target.GetComponent<Target>().difficulty = difficulty;
            GameObject brick = Instantiate(target, pos, transform.rotation);
            gameManager.updateTotalTargets();
        }
    }

    public void giveRecordings(List<float> timings, List<int> sides)
    {
        spawnTimings = timings;
        this.sides = sides;
        stopSpawn = false;
        this.enabled = false;
        this.enabled = true;
    }

    
}
