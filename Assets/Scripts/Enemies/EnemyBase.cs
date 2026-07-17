using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;  


namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float startLife = 10f;

        [SerializeField] private float _currentLife;

        [Header("Start Animation")]
        public float startAnimationDuration = 0.2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

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
            BornAnimation();
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

        #region ANIMATIONS

        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }


        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                OnDamage(5.0f);
            }

        }

    }
}