using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public int damageAmount = 1;
    public float speed = 50f;


    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
           
    }
}
