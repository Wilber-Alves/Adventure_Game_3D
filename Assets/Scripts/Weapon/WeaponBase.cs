using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WeaponBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = 0.3f;
    private Coroutine _currentCoroutine;
    public KeyCode keyCode = KeyCode.Z;

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(keyCode))
        {
            if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        }
       
    }

    private IEnumerator StartShoot()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    private void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
    }
}
