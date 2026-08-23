using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDGEE.StateMachine;
using DG.Tweening;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]

        public float startAnimationDuration = 0.5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]

        public int attackAmount = 5;
        public float timeBetweenAttacks = 0.3f;

        [Tooltip("Arma do boss, responsável pelo disparo real durante o ATTACK.")]
        public WeaponBase weapon;

        public float speed = 5f;
        public List<Transform> waypoints;

        public EnemyBoss enemyBoss;

        [Header("Look At Player")]
        [Tooltip("Referência ao player. Se deixado vazio, é preenchido automaticamente pela tag 'Player'.")]
        public Transform player;

        [Tooltip("Velocidade de rotação em graus/segundo ao olhar para o player. Se 0, a rotação é instantânea.")]
        public float lookAtRotationSpeed = 0f;

        private bool isActive = false;

        private StateMachine<BossAction> stateMachine;

        [SerializeField] private Renderer[] bossRenderers;

        private void Awake()
        {
            Init();
            if (enemyBoss != null)
                enemyBoss.OnBossKilled += OnBossKill;

            if (bossRenderers == null || bossRenderers.Length == 0)
                bossRenderers = GetComponentsInChildren<Renderer>();

            SetVisible(false); 

            if (player == null)
            {
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
                if (playerObj != null)
                    player = playerObj.transform;
            }
        }
        private void SetVisible(bool visible)
        {
            foreach (var rend in bossRenderers)
            {
                rend.enabled = visible;
            }
        }


        public void Activate()
        {
            SetVisible(true); 
            isActive = true;
            StartInitiAnimation();
            SwitchState(BossAction.INIT);
        }

      
        private void Update()
        {
            stateMachine.Update();
            if (isActive)
            {
                LookAtPlayer();
            }
        }
        private void LookAtPlayer()
        {
            if (player == null) return;

            Vector3 direction = player.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.0001f) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            if (lookAtRotationSpeed <= 0f)
            {
                transform.rotation = targetRotation;
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, lookAtRotationSpeed * Time.deltaTime);
            }
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }

        private void OnBossKill(EnemyBoss e)
        {
            SwitchState(BossAction.DEATH); 
        }


        #region ATTACK

        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));

        }

        IEnumerator AttackCoroutine(Action endCallback)
        {
            int attacks = 0;
            while (attacks < attackAmount)
            {
                attacks++;
                transform.localScale = Vector3.one; // garante ponto de partida limpo antes do pulso
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);

                if (weapon != null)
                {
                    weapon.Shoot();
                }

                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            if (endCallback != null)
            {
                endCallback.Invoke();
            }
        }
        #endregion

        #region WALK

        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            if (onArrive != null)
            {
                onArrive.Invoke();
            }
        }

        #endregion

        #region ANIMATION

        public void StartInitiAnimation()
        {
            //transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }
        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }
        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }

        #endregion

        #region STATE MACHINE

        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }

        #endregion
    }
}