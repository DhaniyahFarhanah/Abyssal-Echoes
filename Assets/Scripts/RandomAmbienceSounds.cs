using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAmbienceSounds : MonoBehaviour
{
    public AudioSource ambienceSoundSource;
    public AudioClip[] AmbienceAudioClips;

    public float randomDelayMin;
    public float randomDelayMax;
    public float randomDelay;
    public float elapsedTime;
    public float panDivider;

    public float clipLength;

    public bool soundDone;
    public bool startDelay;
    int randomSound;
    public int soundBehaviour;

    float volumeElapsedTime;
    float panElapsedTime;
    bool increasingSound;

    // Start is called before the first frame update
    void Start()
    {
        randomDelay = Random.Range(randomDelayMin, randomDelayMax);
        soundDone = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ambienceSoundSource.isPlaying);
        if (soundDone)
        {
            randomDelay = Random.Range(randomDelayMin, randomDelayMax);
            elapsedTime = randomDelay;
            startDelay = true;
            soundDone = false;
        }

        if (startDelay)
        {
            if(elapsedTime >= 0f)
            {
                elapsedTime -= Time.deltaTime;
            }
            else if(elapsedTime <= 0f)
            {
                startDelay = false;
                soundDone = false;
                
                PlayRandom();
            }
        }

        Volume();
        Panning();
       
    }

    void Volume()
    {
        if (increasingSound)
        {
            if (volumeElapsedTime < clipLength/4)
            {
                volumeElapsedTime += Time.deltaTime;
                ambienceSoundSource.volume = Mathf.Lerp(0f, 0.7f, volumeElapsedTime / (clipLength/4));
            }
            else
            {
                ambienceSoundSource.volume = 0.7f; // Ensure it ends at the left
            }
        }

        else if (!increasingSound)
        {
            if (volumeElapsedTime < clipLength / 4)
            {
                volumeElapsedTime += Time.deltaTime;
                ambienceSoundSource.volume = Mathf.Lerp(0.7f, 0f, volumeElapsedTime / (clipLength / 4));
            }
            else
            {
                ambienceSoundSource.volume = 0f; // Ensure it ends at the left
            }
        }
    }

    void Panning()
    {
        switch (soundBehaviour)
        {
            case 1: //left to right
                if (increasingSound)
                {
                    if (panElapsedTime < clipLength / panDivider)
                    {
                        panElapsedTime += Time.deltaTime;
                        ambienceSoundSource.panStereo = Mathf.Lerp(-1.0f, 0f, panElapsedTime / (clipLength / panDivider));
                    }
                    else
                    {
                        ambienceSoundSource.panStereo = 0f; // Ensure it ends at center
                    }
                }

                else if (!increasingSound)
                {
                    if (panElapsedTime < clipLength / panDivider)
                    {
                        panElapsedTime += Time.deltaTime;
                        ambienceSoundSource.panStereo = Mathf.Lerp(0f, 1.0f, panElapsedTime / (clipLength / panDivider));
                    }
                    else
                    {
                        ambienceSoundSource.panStereo = 1.0f; // Ensure it ends at the right
                    }
                }

                break;

            case 2: //right to left
                if (increasingSound)
                {
                    if (panElapsedTime < clipLength / panDivider)
                    {
                        panElapsedTime += Time.deltaTime;
                        ambienceSoundSource.panStereo = Mathf.Lerp(1f, 0f, panElapsedTime / (clipLength / panDivider));
                    }
                    else
                    {
                        ambienceSoundSource.panStereo = 0f; // Ensure it ends at center
                    }
                }

                else if (!increasingSound)
                {
                    if (panElapsedTime < clipLength / panDivider)
                    {
                        panElapsedTime += Time.deltaTime;
                        ambienceSoundSource.panStereo = Mathf.Lerp(0f, -1.0f, panElapsedTime / (clipLength / panDivider));
                    }
                    else
                    {
                        ambienceSoundSource.panStereo = -1.0f; // Ensure it ends at the right
                    }
                }

                break;

            case 3: //right pan
                ambienceSoundSource.panStereo = 1.0f;
                break;

            case 4: //left pan
                ambienceSoundSource.panStereo = -1.0f;
                break;

            default: //nothing
                ambienceSoundSource.panStereo = 0f;
                break;
        }
    }

    void PlayRandom()
    {
        randomSound = Random.Range(0, AmbienceAudioClips.Length); //get random clip to play
        soundBehaviour = Random.Range(0, 3); //get type of sound behaviour

        ambienceSoundSource.clip = AmbienceAudioClips[randomSound];
        clipLength = ambienceSoundSource.clip.length;

        if (!ambienceSoundSource.isPlaying)
        {
            ambienceSoundSource.Play();
        }

        StartCoroutine(PlaySound(clipLength));
    }
    IEnumerator PlaySound(float delay)
    {
        soundBehaviour = Random.Range(1, 5);
        increasingSound = true;
        volumeElapsedTime = 0f;
        panElapsedTime = 0f;
        yield return new WaitForSeconds(delay / 3);

        ambienceSoundSource.volume = 1f;
        yield return new WaitForSeconds(delay / 3);

        increasingSound = false;
        volumeElapsedTime = 0f;
        panElapsedTime = 0f;
        yield return new WaitForSeconds(delay / 3);
        soundDone = true;
    }
}
