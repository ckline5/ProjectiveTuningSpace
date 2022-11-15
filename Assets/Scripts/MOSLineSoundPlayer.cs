using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOSLineSoundPlayer : MonoBehaviour
{
    MOSLine parent;
    int sampleFreq = 48000;
    float baseFrequency = 440;
    //float _frequency = 0;
    //float Frequency
    //{
    //    get
    //    {
    //        //if (_frequency == 0)
    //        //{
    //        //    _frequency = baseFrequency * Mathf.Pow(2, parent.cents / 1200);
    //        //}
    //        //Debug.Log(_frequency);
    //        //return _frequency;
    //        return baseFrequency * Mathf.Pow(2, parent.cents / 1200);
    //    }
    //}
    float frequency;

    AudioClip ac;
    AudioSource source;

    private void Awake()
    {
        parent = transform.GetComponentInParent<MOSLine>();
        source = GetComponent<AudioSource>();
    }

    public void SetupSound()
    {
        frequency = Mathf.Pow(2, parent.cents / 1200);
        Debug.Log(parent.cents + " - " + frequency);
        source.pitch = frequency;
    }

    //old manual sine wave way
    public void SetupSoundManualSine()
    {
        frequency = baseFrequency * Mathf.Pow(2, parent.cents / 1200);
        Debug.Log(parent.cents + " - " + frequency);
        float[] samples = new float[sampleFreq];
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = Mathf.Sin(Mathf.PI * 2 * i * frequency / sampleFreq);
        }
        ac = AudioClip.Create(parent.cents.ToString(), samples.Length, 1, sampleFreq, false);
        ac.SetData(samples, 0);
    }


    public void PlaySound()
    {
        Debug.Log(parent.cents);
        //play a sound too
        if (frequency == 0)
            SetupSound();
        //AudioSource.PlayClipAtPoint(ac, FPSFlyer.Instance.transform.position);
        source.Play();
    }

    public void OnMouseEnter()
    {
        if (!UIHandler.Instance.mouseInUI)
            if (parent.lineType == MOSLine.LineType.LINE)
            {
                parent.line.startColor = Color.cyan;
                parent.line.endColor = parent.line.startColor;
            }
    }

    public void OnMouseOver()
    {
        if (UIHandler.Instance.mouseInUI)
            if (parent.lineType == MOSLine.LineType.LINE)
            {
                parent.line.startColor = Color.black;
                parent.line.endColor = parent.line.startColor;
            }
    }

    private void OnMouseExit()
    {
        if (parent.lineType == MOSLine.LineType.LINE)
        {
            parent.line.startColor = Color.black;
            parent.line.endColor = parent.line.startColor;
        }
    }
}
