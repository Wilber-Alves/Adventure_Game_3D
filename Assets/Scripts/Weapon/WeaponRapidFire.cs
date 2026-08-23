using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;

public class WeaponRapidFire : WeaponBase
{
    public List<UIWeaponUpdater> uIWeaponUpdaters;

    public float timeBTWShoot = 0.2f;
    public float maxShoot = 5f;
    public float timeToRecharge = 0.2f;


    private float _currentShoot;
    private bool _isRecharging = false;

    private void Awake()
    {
        GetAllUIs();
    }


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
                UpdateUI();
                yield return new WaitForSeconds(timeBTWShoot);
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
            uIWeaponUpdaters.ForEach(i => i.UpdateValue(time / timeToRecharge));
            yield return null;
        }

        // Reseta a contagem de tiros e permite disparar novamente
        _currentShoot = 0;
        _isRecharging = false;
    }


    private void UpdateUI()
    {
        uIWeaponUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoot));

    }

    private void GetAllUIs()
    {
        uIWeaponUpdaters = GameObject.FindObjectsByType<UIWeaponUpdater>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();

    }

}