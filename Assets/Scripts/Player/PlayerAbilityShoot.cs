using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public WeaponBase weaponBase;

    protected override void Init()
    {
        base.Init();
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }

    private void StartShoot()
    {
        weaponBase.StartShoot();
        Debug.Log("Shoot");
    
    }

    private void CancelShoot()
    {
        weaponBase.StopShoot();
        Debug.Log("Shoot");
    }


}
