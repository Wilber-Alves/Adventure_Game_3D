using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WeaponBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = 0.3f;
    public float speed = 50f;
    private Coroutine _currentCoroutine;

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
    }

    public void StartShoot()
    {
        if (_currentCoroutine == null) // Verifica se já está disparando
        {
            _currentCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null; // Reseta a referência
        }
    }
}