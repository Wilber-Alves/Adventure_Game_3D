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
        public class BossStateInit : BossStateBase
        {
            public override void OnStateEnter(params object[] objs)
            {
                base.OnStateEnter(objs);
                Debug.Log("Boss:  " + boss);
            }
            
        }

    }
}