using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    [Header("Weapons")]
    public WeaponBase[] weaponBases; // Array with 4 weapons
    public Transform weaponPosition;

    private WeaponBase _currentWeapon;
    private int _currentWeaponIndex = 0;

    protected override void Init()
    {
        base.Init();

        // Create the first weapon
        CreateWeapon(_currentWeaponIndex);

        // Shoot input
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();

        // Weapon switch inputs (the 4 Actions created)
        inputs.Player_Weapons.SelectWeapon1.performed += ctx => SwitchWeapon(0);
        inputs.Player_Weapons.SelectWeapon2.performed += ctx => SwitchWeapon(1);
        inputs.Player_Weapons.SelectWeapon3.performed += ctx => SwitchWeapon(2);
        inputs.Player_Weapons.SelectWeapon4.performed += ctx => SwitchWeapon(3);
    }

    private void OnDestroy()
    {
        // Clear events
        if (inputs != null)
        {
            inputs.Gameplay.Shoot.performed -= ctx => StartShoot();
            inputs.Gameplay.Shoot.canceled -= ctx => CancelShoot();

            inputs.Player_Weapons.SelectWeapon1.performed -= ctx => SwitchWeapon(0);
            inputs.Player_Weapons.SelectWeapon2.performed -= ctx => SwitchWeapon(1);
            inputs.Player_Weapons.SelectWeapon3.performed -= ctx => SwitchWeapon(2);
            inputs.Player_Weapons.SelectWeapon4.performed -= ctx => SwitchWeapon(3);
        }
    }

    private void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponBases.Length)
            return;

        if (weaponIndex == _currentWeaponIndex)
            return;

        // Destroy the current weapon
        if (_currentWeapon != null)
        {
            Destroy(_currentWeapon.gameObject);
        }

        // Create the new weapon
        _currentWeaponIndex = weaponIndex;
        CreateWeapon(_currentWeaponIndex);

        Debug.Log($"Weapon {weaponIndex + 1} equipped!");
    }

    private void CreateWeapon(int index)
    {
        _currentWeapon = Instantiate(weaponBases[index], weaponPosition);
        _currentWeapon.transform.localPosition = Vector3.zero;
        _currentWeapon.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.StartShoot();
            Debug.Log("Shoot");
        }
    }

    private void CancelShoot()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.StopShoot();
            Debug.Log("Stop Shoot");
        }
    }
}