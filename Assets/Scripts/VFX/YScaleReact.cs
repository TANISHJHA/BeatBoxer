using Assets.Scripts.VFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YScaleReact : VisualizationEffectBase
{
    private Vector3 yVec;
    public float maxHeight;
    public float scaleControl = 0.5f;
    public override void Start()
    {
        base.Start();
        yVec = transform.localScale;
        yVec.Set(0, yVec.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 resetter = transform.localScale.y * Vector3.up;
        if (transform.position.y < yVec.y || transform.position.y > maxHeight)
        {
            float yDiff = yVec.y - transform.localScale.y;
            Vector3 resetVector = Vector3.up * yDiff;
            transform.localScale += resetVector;
        }
        float audioData = GetAudioData();
        float scaler = 0;
        if (audioData != 0)
        {
            scaler = maxHeight / audioData * scaleControl;
        }
        Vector3 yScaler = Vector3.up * scaler;
        transform.localScale += yScaler;


    }
}
