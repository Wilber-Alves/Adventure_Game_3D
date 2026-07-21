using UnityEngine;


namespace Enemy
{
    public class EnemyShoots : EnemyBase
    {
        public WeaponBase weapon;

        protected override void Init()
        {
            base.Init();
            weapon.StartShoot();
        }
    }
}
