using Enemy;
using System;
using UnityEngine;

namespace Boss
{
    public class EnemyBoss :  EnemyBase
    {
        public Action<EnemyBoss> OnBossKilled;

        protected override void Kill()
        {
            if (IsDead) return; // evita disparar o evento mais de uma vez

            Debug.Log($"[EnemyBoss] Kill() chamado. InstanceID={GetInstanceID()} | Tem listeners? {(OnBossKilled != null)}");

            base.Kill();
            OnBossKilled?.Invoke(this);

            Debug.Log($"[EnemyBoss] OnBossKilled invocado. InstanceID={GetInstanceID()}");
        }
    }
}