using Assets.Scripts.VFX;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class YPosReactEffect : VisualizationEffectBase
{
    private Vector3 yVec;
    public float maxHeight;

    public override void Start()
    {
        base.Start();
        yVec = transform.position;
        yVec.Set(0, yVec.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 resetter = transform.position.y * Vector3.up;
            if (transform.position.y < yVec.y || transform.position.y > maxHeight)
            {
                float yDiff = yVec.y - transform.position.y;
                Vector3 resetVector = Vector3.up * yDiff;
                transform.position += resetVector;
            }
            float audioData = GetAudioData();
            float scaler = 0;
            if (audioData != 0)
            {
                scaler = maxHeight / audioData;
            }
            Vector3 yScaler = Vector3.up * scaler;
            transform.position += yScaler;

        
    }
}
