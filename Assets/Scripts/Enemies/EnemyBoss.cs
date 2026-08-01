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
            base.Kill();
            OnBossKilled?.Invoke(this);
        }
    }
}