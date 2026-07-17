using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;  
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {

        public new Collider collider;

        [SerializeField] private float _currentLife;

        public float startLife = 10f;

        [Header("Animation")]

        [SerializeField] private AnimationBase _animationBase;

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
            if (collider != null)
            {
                collider.enabled = false;
            }
            Destroy(gameObject, 1.2f);
            PlayAnimationByTrigger(AnimationType.DEATH);
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
        public void PlayAnimationByTrigger(AnimationType animationType)
        { 
            _animationBase.PlayAnimationByTrigger(animationType);

        }


        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                OnDamage(5.0f);
            }

        }

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

    }
}