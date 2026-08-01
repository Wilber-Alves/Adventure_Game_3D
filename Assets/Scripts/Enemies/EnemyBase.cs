using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;  
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [Header("Physics and VFX")]
        public new Collider collider;
        public FlashColor flashColor;
        public ParticleSystem damageParticleSystem;


        [Header("Health")]
        [SerializeField] private float _currentLife;
        public float startLife = 10f;
        public bool IsDead { get; private set; } // TESTE, Se nao funcionar, retirar e manter script original

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = 0.2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;


        [Header("Look at Player")] // TESTE, Se nao funcionar, retirar e manter script original
        public bool lookAtPlayer = false;
        public float lookAtRotationSpeed = 8f;

        [Header("KnockBack")] // TESTE, Se nao funcionar, retirar e manter script original
        public float knockbackDistance = 0.3f;
        public float knockbackDuration = 0.15f;

        protected PlayerController playerController;// TESTE, antes era private e o playerController possuia o _ antes

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            playerController = FindFirstObjectByType<PlayerController>();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
            IsDead = false; // TESTE, Se nao funcionar, retirar e manter script original
        }

        protected virtual void Init()
        {
            ResetLife();
            if ((startWithBornAnimation))
            {
                BornAnimation();
            }

        }

        protected virtual void Kill()
        {
            if (IsDead) return; // TESTE, Se nao funcionar, retirar e manter script original
            IsDead = true; // TESTE, Se nao funcionar, retirar e manter script original
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
            if (IsDead) return; // TESTE, Se nao funcionar, retirar e manter script original

            if (flashColor != null)
            {
                flashColor.Flash();
            }
            if (damageParticleSystem != null)
            {
                damageParticleSystem.Emit(15);
            }

            // transform.position -= transform.forward * -1f;
            ApplyKnockback(); // TESTE, Se nao funcionar, retirar e manter script original

            _currentLife -= damage;
            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        public virtual void ApplyKnockback() // TESTE, Se nao funcionar, retirar e manter script original
        {
            transform.DOComplete(); // força qualquer tween pendente (escala, posição) a terminar no valor final, em vez de travar no meio
            transform.DOMove(transform.position - transform.forward * knockbackDistance, knockbackDuration);
        }


        #region ANIMATIONS

        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            if (_animationBase != null) // TESTE, Se nao funcionar, retirar e manter script original
            {
                _animationBase.PlayAnimationByTrigger(animationType);
            }
        }

        #endregion

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

        protected virtual void OnCollisionEnter(Collision collision) // TESTE,  antes era sp private void
        {  
           if (collision.gameObject.CompareTag("Player"))
           {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.Damage(1f);
                }
           }
        }

        public virtual void Update() 
        {
            if (lookAtPlayer && playerController != null)
            {
                //transform.LookAt(playerController.transform.position);
                LookAtPlayer();
            }
        }

        protected void LookAtPlayer() // TESTE, Se nao funcionar, retirar e manter script original
        {
            Vector3 direction = playerController.transform.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.0001f) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookAtRotationSpeed);
        }

    }
}
