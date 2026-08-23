using UnityEngine;

namespace Enemy
{
    public class EnemyShoots : EnemyWalk // TESTE, alterado de EnemyBase para EnemyWalk, pois inimigos atiradores tambem andam pelos waypoints
    {
        public WeaponBase weapon;

        protected override void Init()
        {
            base.Init();

            // TESTE, guarda de seguranca contra weapon nao atribuida no Inspector
            if (weapon == null)
            {
                Debug.LogWarning($"{name}: WeaponBase nao atribuida em EnemyShoots.");
                return;
            }

            weapon.StartShoot();
        }

        protected virtual void OnDisable() // TESTE, garante que o inimigo pare de atirar ao ser desativado (ex: morte, pool de objetos)
        {
            if (weapon != null)
            {
                weapon.StopShoot();
            }
        }
    }
}