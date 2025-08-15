using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject target;
    public GameObject targetZone;
    public SpeedBag speedBag;
    public int numTargets = 100;
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
            pos += Vector3.right * 6;
        }
        GameObject brick = Instantiate(target, pos, transform.rotation);
        currentSpawn++; 
        if (currentSpawn < numTargets)
        {
            Invoke(nameof(Spawn), spawnTimings[currentSpawn]);
        }
    } 

    public void beatSpawn()
    {
        Vector3 pos = targetZone.transform.position;
        if (spawnSide == 0)
        {
            spawnSide = 1;
        }
        else
        {
            spawnSide = 0;
            pos += Vector3.right * 6;
        }
        GameObject brick = Instantiate(target, pos, transform.rotation);
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
