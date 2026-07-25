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

        // TESTE: adicionado campo de arma para permitir disparo real do boss durante o ATTACK (antes não existia integração com WeaponBase).
        [Tooltip("Arma do boss, responsável pelo disparo real durante o ATTACK.")]
        public WeaponBase weapon;

        public float speed = 5f;
        public List<Transform> waypoints;

        public HealthBase healthBase;

        // TESTE: adicionados campos para permitir que o boss mire continuamente no player (antes não existia referência direta nem controle de velocidade de rotação).
        [Header("Look At Player")]
        [Tooltip("Referência ao player. Se deixado vazio, é preenchido automaticamente pela tag 'Player'.")]
        public Transform player;

        [Tooltip("Velocidade de rotação em graus/segundo ao olhar para o player. Se 0, a rotação é instantânea.")]
        public float lookAtRotationSpeed = 0f;

        // TESTE: flag para controlar quando o boss deve mirar continuamente (só depois de Activate()).
        private bool isActive = false;

        private StateMachine<BossAction> stateMachine;

        [SerializeField] private Renderer[] bossRenderers;

        private void Awake()
        {
            Init();
            healthBase.OnKilled += OnBossKill;

            if (bossRenderers == null || bossRenderers.Length == 0)
                bossRenderers = GetComponentsInChildren<Renderer>();

            SetVisible(false); // boss começa invisível

            // TESTE: estado inicial nunca era setado, boss ficava parado (_currentState == null) até algum botão de debug ser clicado.
            // SwitchState(BossAction.INIT);// TESTE: removido o SwitchState(BossAction.INIT) automático daqui.
            // Agora quem dispara o início da sequência é o BossTrigger, via Activate(),
            // quando o player entra na área de trigger do boss.

            // TESTE: adicionado preenchimento automático do player pela tag "Player", caso não seja setado manualmente no Inspector.
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
            // TESTE: removido o LookAt único feito aqui dentro do Activate().
            // Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
            // if (player != null)
            // {
            //     transform.LookAt(player);
            // }
            // Motivo: a mira agora é contínua (mira o player o tempo todo em WALK e ATTACK),
            // controlada por LookAtPlayer() chamado a cada frame em Update().

            SetVisible(true); // aparece

            // TESTE: isActive marca que a partir daqui o boss deve olhar continuamente para o player em Update().
            isActive = true;

            SwitchState(BossAction.INIT);
        }

        // TESTE: Update() não existia em BossBase, então stateMachine.Update() (e OnStateStay dos estados) nunca era chamado.
        private void Update()
        {
            stateMachine.Update();

            // TESTE: chamada de LookAtPlayer() a cada frame enquanto o boss estiver ativo,
            // garantindo mira contínua durante WALK e ATTACK (antes só mirava uma vez no Activate()).
            if (isActive)
            {
                LookAtPlayer();
            }
        }

        // TESTE: novo método responsável pela mira contínua no player.
        // Se lookAtRotationSpeed <= 0, rotação é instantânea (comportamento antigo do transform.LookAt).
        // Se lookAtRotationSpeed > 0, rotação suave em graus/segundo via Quaternion.RotateTowards.
        private void LookAtPlayer()
        {
            if (player == null) return;

            Vector3 direction = player.position - transform.position;
            direction.y = 0f; // mantém o boss "em pé", sem inclinar olhando pra cima/baixo

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

        private void OnBossKill(HealthBase h)
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
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);

                // TESTE: adicionado disparo real da arma a cada ciclo de ataque (antes só havia o efeito de escala, sem tiro de verdade).
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

        public void GoToRandomPoint(Action onArrive = null) // exemplo de funcáo com callBack chamado Action onArrive
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
                onArrive.Invoke(); // codigo onde ocorre a chamada do callBack criado
            }
        }

        #endregion

        #region ANIMATION

        public void StartInitiAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();

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