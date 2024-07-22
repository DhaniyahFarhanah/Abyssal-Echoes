using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource footstep;
    public AudioClip[] footstepClips;
    public RumbleManager manager;
    public GameObject referenceObject;

    bool PlayOnce;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (referenceObject.activeInHierarchy)
        {
            if (PlayOnce)
            {
                footstep.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
                PlayOnce = false;
            }
            

            if (manager.left)
            {
                footstep.panStereo = -1f;
            }

            else if(manager.right)
            {
                footstep.panStereo = 1f;
            }
        }

        if (!referenceObject.activeInHierarchy)
        {
            PlayOnce = true;
        }


    }
}
