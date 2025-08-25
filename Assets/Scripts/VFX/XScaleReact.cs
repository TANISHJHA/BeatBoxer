using Assets.Scripts.VFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XScaleReact : VisualizationEffectBase
{
    private Vector3 xVec;
    public float maxLength;
    public float scaleControl = 0.5f;
    public override void Start()
    {
        base.Start();
        xVec = transform.localScale;
        xVec.Set(xVec.x, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 resetter = transform.localScale.x * Vector3.right;
        if (transform.position.x < xVec.x || transform.localScale.x > maxLength)
        {
            float xDiff = xVec.x - transform.localScale.x;
            Vector3 resetVector = Vector3.right * xDiff;
            transform.localScale += resetVector;
        }
        float audioData = GetAudioData();
        float scaler = 0;
        if (audioData != 0)
        {
            scaler = maxLength / audioData * scaleControl;
        }
        Vector3 xScaler = Vector3.right * scaler;
        transform.localScale += xScaler;


    }
}
