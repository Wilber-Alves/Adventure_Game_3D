using DG.Tweening;
using EDGEE.StateMachine;
using UnityEngine;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter(params object[] objs) // poderia ser "object o = null" ou "params object[] objs"
        {
            {
                base.OnStateEnter(objs);
                boss = (BossBase)objs[0]; // aqui tem uma diferença, eu preciso colocar o primeiro da lista do array, antes bastava colocar só "o"
            }
        }
    }
    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.transform.localScale = Vector3.zero; // garante ponto de partida visível e controlado
            boss.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack).OnComplete(() => {boss.SwitchState(BossAction.WALK);});
        }

    }
    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.GoToRandomPoint(OnArrive); // o callback de OnArrive passou para cá
        }

        private void OnArrive()
        {
            boss.SwitchState(BossAction.ATTACK);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exit Walk");
            boss.StopAllCoroutines();
        }

    }
    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StartAttack(EndAttacks);
        }

        private void EndAttacks()
        {
            boss.SwitchState(BossAction.WALK);

        }
        public override void OnStateExit()
        {
            Debug.Log("Exit Attack");
            base.OnStateExit();
            boss.StopAllCoroutines();
        }

    }
    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            Debug.Log("Enter Death");
            boss.transform.DOKill(); // garante que nenhuma tween antiga (ataque/nascimento) continue rodando por cima
            boss.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack); // encolhe suavemente até sumir, sem escala negativa
        }
    }
}