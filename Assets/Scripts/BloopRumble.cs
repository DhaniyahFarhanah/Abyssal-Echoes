using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BloopRumble : MonoBehaviour
{
    public GameObject player;
    public float distancebetween;
    public float offset;
    public float maxDist;
    public float rumbleVal;

    bool outOfRange;

    private Gamepad pad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pad = Gamepad.current;
        distancebetween = Vector3.Distance(player.transform.position, transform.position);

        if (pad != null)
        {

            if (distancebetween < maxDist)
            {
                rumbleVal = distancebetween * 0.05f;
                pad.SetMotorSpeeds(rumbleVal, rumbleVal);
            }

            else if (distancebetween > maxDist)
            {
                outOfRange = true;
                if (outOfRange)
                {
                    pad.SetMotorSpeeds(0f, 0f);
                }

            }
        }

    }
}
