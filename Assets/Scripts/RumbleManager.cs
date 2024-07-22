using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    //this is such disgusting code wtf but submission is in 2 days

    private Gamepad pad;
    public bool play;
    public GameObject referenceObject;
    
    [Header("Rumble")]
    public float lowFreq;
    public float highFreq;
    public float duration;

    public bool left;
    public bool right;

    public void Start()
    {
        play = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(referenceObject!= null)
        {
            if (referenceObject.activeInHierarchy)
            {
                if (left)
                {
                    PulseRumble(highFreq, 0f, duration);

                }
                if (right)
                {
                    PulseRumble(0f, highFreq, duration);
                }
                
            }
        }
        else
        {
            if (play)
            {
                PulseRumble(lowFreq, highFreq, duration);
            }
        }
       
        
    }

    private IEnumerator StopRumble(float duration, Gamepad pad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pad.SetMotorSpeeds(0f, 0f);
        play = false;
    }

    public void PulseRumble(float lowFreq, float highFreq, float duration)
    {
        pad = Gamepad.current;

        if (pad != null)
        {
            //start rumble
            pad.SetMotorSpeeds(lowFreq, highFreq);

            //stop rumble
            StartCoroutine(StopRumble(duration, pad));

        }
    }



}
