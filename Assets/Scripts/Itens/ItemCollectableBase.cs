using UnityEngine;
using System.Collections.Generic;
using System.Collections;


namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public ItemType itemType;

        public string compareTag = "Player";

        [Header("Particles")]
        public new ParticleSystem particleSystem;

        [Header("Sounds")]
        public AudioSource audioSource;
       

        private void Awake()
        {
            if (particleSystem != null) particleSystem.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
                OnCollect();
            }
        }

        protected virtual void Collect()
        {
            if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().enabled = false;
            if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = false;

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
            ItemManager.Instance.AddByType(itemType);
        }
    }
}
