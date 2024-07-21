using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Breathing : MonoBehaviour
{
    private Gamepad pad;

    public GameObject bubbles1;
    public GameObject bubbles2;

    public float bubbleLifetime;
    float bubbleDuration;

    public float breathingInterval;
    float countdown;

    // Start is called before the first frame update
    void Start()
    {
        bubbleDuration = bubbleLifetime;
        countdown = breathingInterval;
    }

    // Update is called once per frame
    void Update()
    {
        pad = Gamepad.current;
        if (countdown >= 0)
        {
            if (bubbleDuration >= 0)
            {
                bubbleDuration -= Time.deltaTime;
            }
            else if (bubbleDuration < 0)
            {
                bubbles1.SetActive(false);
                bubbles2.SetActive(false);
            }

            countdown -= Time.deltaTime;
        }

        else if (countdown < 0)
        {
            bubbles1.SetActive(true);
            bubbles2.SetActive(true);

            countdown = breathingInterval;
            bubbleDuration = bubbleLifetime;
        }
    }
}
