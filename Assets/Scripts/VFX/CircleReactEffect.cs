using Assets.Scripts.VFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleReactEffect : VisualizationEffectBase
{
    private Vector3 radius;
    public float scaleFactor = 1;

    // Update is called once per frame
    void Update()
    {
        float audioData = GetAudioData();
        radius.Set(audioData, audioData, audioData); 
        transform.localScale = radius*scaleFactor;
    }
}
