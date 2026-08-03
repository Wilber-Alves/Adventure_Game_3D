using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ItemCollectableBase : MonoBehaviour
{

    public string compareTag = "Player";
    
    [Header("Particles")]
    public ParticleSystem particleSystem;

    [Header("Sounds")]
    public AudioSource audioSource;
    

    private void Awake()
    {
        if (particleSystem != null) particleSystem.transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(compareTag))
        {
            Collect();
            OnCollect();
        }
    }

    protected virtual void Collect()
    {
        if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().enabled = false;
        if (GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = false;

        OnCollect();    
    }
    protected virtual void OnCollect()
    { 
        if (particleSystem != null) particleSystem.Play();

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.spatialBlend = 0f;
            audioSource.Play();

            Destroy(gameObject, 0.5f);

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
