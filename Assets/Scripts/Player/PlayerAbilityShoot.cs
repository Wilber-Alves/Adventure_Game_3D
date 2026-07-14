using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{

    public WeaponBase weaponBase;
    public Transform weaponPosition;

    private WeaponBase _currentWeapon;

    protected override void Init()
    {
        base.Init();
        CreateWeapon();
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }

    private void CreateWeapon()
    { 
        _currentWeapon = Instantiate(weaponBase, weaponPosition);
        _currentWeapon.transform.localPosition = _currentWeapon.transform.localEulerAngles = Vector3.zero;
    }


    private void StartShoot()
    {
        _currentWeapon.StartShoot();
        Debug.Log("Shoot");
    
    }

    private void CancelShoot()
    {
        _currentWeapon.StopShoot();
        Debug.Log("Shoot");
    }


}
