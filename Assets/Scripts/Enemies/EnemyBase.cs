using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float startLife = 100f;

        [SerializeField] private float _currentLife;

        private void Awake()
        {
            Init();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init() 
        { 
            ResetLife();
        }

        protected virtual void Kill()
        {
            OnKill();

        }
        protected virtual void OnKill()
        {
            Destroy(gameObject);
        }

        public void OnDamage(float damage)
        {
            _currentLife -= damage;
            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                OnDamage(5.0f);
            }

        }

    }
}