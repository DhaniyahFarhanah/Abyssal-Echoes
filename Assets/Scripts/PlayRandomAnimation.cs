using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAnimation : MonoBehaviour
{

    public Animator anima;
    public float randomDelayMin;
    public float randomDelayMax;
    bool animationDone;

    public AnimationClip[] clip;

    // Start is called before the first frame update
    void Start()
    {
        animationDone = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (animationDone)
        {
            RandomDelay();
        }
    }

    void RandomDelay()
    { 
        float delay = Random.Range(randomDelayMin, randomDelayMax);
        int index = Random.Range(0, clip.Length);

        StartCoroutine(PlayAnim(delay, index));
        
    }

    IEnumerator PlayAnim(float delay, int i)
    {
        animationDone = false;
        yield return new WaitForSeconds(delay);
        anima.Play(clip[i].name);
        animationDone = true;
    }
}
