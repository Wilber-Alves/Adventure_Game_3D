using UnityEngine;
using EDGEE.StateMachine;

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
            boss.StartInitiAnimation();
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
            boss.transform.localScale = Vector3.one * - 0.2f; // apenas um feedback para entre os estados de morte. 
        }

    }

}