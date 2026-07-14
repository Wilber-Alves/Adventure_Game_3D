using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

public class WeaponShootLimited : WeaponBase
{
    public float maxShoot = 5f;
    public float timeToRecharge = 1f;

    private float _currentShoot;
    private bool _isRecharging = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !_isRecharging) // Verifica se a tecla X foi pressionada
        {
            StartShoot();
        }

        if (Input.GetKeyUp(KeyCode.X)) // Para disparar quando a tecla é solta
        {
            StopShoot();
        }
    }

    protected override IEnumerator ShootCoroutine()
    {
        while (!_isRecharging)
        {
            if (_currentShoot < maxShoot)
            {
                Shoot();
                _currentShoot++;
                yield return new WaitForSeconds(timeBetweenShoot);
            }
            else
            {
                StartRecharge();
            }
        }
    }

    private void StartRecharge()
    {
        if (!_isRecharging)
        {
            StopShoot(); // Para o disparo enquanto recarrega
            _isRecharging = true;
            StartCoroutine(RechargeCoroutine());
        }
    }

    private IEnumerator RechargeCoroutine()
    {
        float time = 0f;

        while (time < timeToRecharge)
        {
            time += Time.deltaTime;
            Debug.Log("Recharging: " + time);
            yield return null;
        }

        // Reseta a contagem de tiros e permite disparar novamente
        _currentShoot = 0;
        _isRecharging = false;
    }
}