using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateOnEnter : MonoBehaviour
{
    public enum Type 
    {
        Particle,
        Animation
    }

    public Type type;

    [Header("Particle Ref")]
    public GameObject ParticleEffect;

    // Start is called before the first frame update
    void Start()
    {
        if (ParticleEffect != null)
        {
            ParticleEffect.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (type == Type.Particle)
            {
                if (ParticleEffect != null)
                {
                    ParticleEffect.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == Type.Particle)
            {
                if (ParticleEffect != null)
                {
                    ParticleEffect.SetActive(false);
                }
            }
        }
    }
}
