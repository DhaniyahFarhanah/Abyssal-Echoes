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
        if(inRange)
        {
            if (PlayAnimOnce && lowFreq > maxFreq * 0.5f)
            {
                
                cam.SetTrigger("Shake");
                Debug.Log("Shake");
                PlayAnimOnce = false;
            }
        }
        
        pad = Gamepad.current;

        if (inRange)
        {
            if (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;

                lowFreq = Mathf.Lerp(lowFreq, maxFreq, Time.deltaTime * rate);


            }
            else
            {
                lowFreq = maxFreq;
            }

            if (pad != null)
            {
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
            PlayAnimOnce = true;

            other.gameObject.GetComponent<PlayerMovement>().panic = true;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (pad != null)
            {
                pad.SetMotorSpeeds(0f, 0f);
            }
            
            inRange = false;
        }
    }
}
