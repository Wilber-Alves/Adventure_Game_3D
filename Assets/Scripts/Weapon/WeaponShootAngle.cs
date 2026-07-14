using UnityEngine;
using System.Collections;

public class WeaponShootAngle : WeaponShootLimited
{
    public int amountPerShoot = 3;
    public float angle = 15f;

    public override void Shoot()
    {

        int mult = 0;

        for(int i = 0; i < amountPerShoot; i++)
        {
            if(i%2 == 0)
            {
                mult++;
            }

            var projectile = Instantiate(prefabProjectile, positionToShoot); // Instantiate the projectile prefab at the shooting position
            projectile.transform.position = positionToShoot.position; // Reset the projectile's position to the origin
            projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (i%2 == 0 ? angle : -angle) * mult; // Set the projectile's rotation based on the angle and the index of the loop

            projectile.speed = speed;
            projectile.transform.parent = null;
        }
    }
}
