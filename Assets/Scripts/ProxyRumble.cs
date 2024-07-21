using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProxyRumble : MonoBehaviour
{
    private Gamepad pad;

    [Header("Rumble")]
    public float maxFreq;
    public float lowFreq;
    public float rate;
    public float duration;
    private float elapsedTime;

    public bool inRange;
    private bool PlayAnimOnce;

    public Animator cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lowFreq > maxFreq * 0.5f && PlayAnimOnce)
        {
            cam.SetTrigger("Shake");
            Debug.Log("Shake");
            PlayAnimOnce = false;
        }
        
        pad = Gamepad.current;

        if(pad!=null)
        {
            if (inRange)
            {
                PlayAnimOnce = true;
                if(elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float t = elapsedTime / duration;

                    lowFreq = Mathf.Lerp(lowFreq, maxFreq, Time.deltaTime * rate);

                    
                }
                else
                {
                    lowFreq = maxFreq;
                }

                pad.SetMotorSpeeds(lowFreq, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //do the proxy rumble
            inRange = true;
            lowFreq = 0;
            

        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pad.SetMotorSpeeds(0f, 0f);
            inRange = false;
        }
    }
}
