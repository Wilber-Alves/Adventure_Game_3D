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
            
    }

}