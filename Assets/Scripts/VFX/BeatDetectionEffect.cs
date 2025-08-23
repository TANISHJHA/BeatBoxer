using Assets.Scripts.VFX;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.XR;
using UnityEngine;

public class BeatDetectionEffect : VisualizationEffectBase
{
    private Vector3 baseScale;
    private Vector3 bigScale;
    private bool[] lastBeats;
    private List<float>[] timings;
    private float time = 0.0f;
    private float[] lastTime;
    private int beatCounter = 0;
    public float timeLimit = 4.0f;
    public int spectrumSize = 64;
    public float scaleFactor;
    public Spawner spawner;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        baseScale = transform.localScale;
        bigScale = baseScale * scaleFactor;
        lastBeats = new bool[spectrumSize];
        timings = new List<float>[spectrumSize]; 
        lastTime = new float[spectrumSize]; 
        for (int i = 0; i < spectrumSize; i++)
        {
            lastTime[i] = 0; 
            timings[i] = new List<float>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        time += Time.deltaTime; 
        if (time >= timeLimit)
        {
            //Make decision on which subband to use to calculate bpm
            float bpm = beatCounter / timeLimit * 60;
            Debug.Log(bpm);
            time = 0.0f;
            for (int i = 0; i < spectrumSize;i++)
            {
                lastTime[i] = 0;
            }
            beatCounter = 0;
        } 
        */
        //We will check every index and record time since last beat for 4 seconds 
        //then choose the most consistent subband to calculate BPM from. 
        bool beatData = GetBeat();
        if (beatData && !lastBeats[0])
        {
            //transform.localScale = bigScale; 
            spawner.beatSpawn();
            lastBeats[0] = true;
            //timings[i].Add(time - lastTime[i]); 
            //lastTime[i] = time;
            //Debug.Log("ya");
            beatCounter++;
        }
        else if (!beatData)
        {
            //transform.localScale = baseScale;
            //spawner.beatSpawn(); 
            lastBeats[0] = false;
        }
  
    } 

    private float CalculateBPM()
    {
        float[] timingAverage = new float[spectrumSize];
        float[] timingTotal = new float[spectrumSize];
        float[] divergenceScore = new float[spectrumSize];
        for (int i = 0; i<spectrumSize; i++)
        {
            timingAverage[i] = 0; 
            timingTotal[i] = 0; 
            divergenceScore[i] = 0;
        }

        for (int i = 0; i < spectrumSize; i++)
        {
            foreach (float timeDiff in timings[i])
            {
                timingTotal[i] += timeDiff;
            }
            timingAverage[i] = timingTotal[i] / timings[i].Count;

            //For each subband calculate divergence from average time difference 
            foreach (float timeDiff in timings[i])
            {
                divergenceScore[i] += timingAverage[i] - timeDiff; //Lower is better
            }
        }
        int bestBand = 0;
        float lowestScore = divergenceScore[0];
        for (int i = 0; i <spectrumSize; i++)
        {
            if (divergenceScore[i] < lowestScore)
            {
                bestBand = i; 
                lowestScore = divergenceScore[i];
            }
        } 

        //Calculate BPM using lowest divergenceScore subband in the last 4 seconds 
        float bpm = 60 / timingAverage[bestBand];

        return bpm;
    }
}
