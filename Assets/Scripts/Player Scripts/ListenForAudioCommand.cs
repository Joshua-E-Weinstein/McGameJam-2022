using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenForAudioCommand : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float db = MicInput.MicLoudnessinDecibels;

        if (db > -30f)
        {
            Debug.Log("ECHO");

        }
        else
        {
            Debug.Log("");
        }

        
        //Debug.Log("Volume is " + MicInput.MicLoudness.ToString("##.#####") + ", decibels is :" + MicInput.MicLoudnessinDecibels.ToString("######"));
        //Debug.Log("Volume is " + NormalizedLinearValue(MicInput.MicLoudness).ToString("#.####") + ", decibels is :" + NormalizedDecibelValue(MicInput.MicLoudnessinDecibels).ToString("#.####"));
    }


    float NormalizedLinearValue(float v)
    {
        float f = Mathf.InverseLerp(.000001f, .001f, v);
        return f;
    }

    float NormalizedDecibelValue(float v)
    {
        float f = Mathf.InverseLerp(-114.0f, 6f, v);
        return f;
    }
}
