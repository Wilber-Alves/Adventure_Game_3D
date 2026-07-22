using UnityEngine;
using EDGEE.StateMachine;

namespace Boss
{

    public enum BossAction
    { 
        INIT,
        IDLE,
        WALK,
        ATTACK
    }

    public class BossBase : MonoBehaviour
    {
        private StateMachine<BossAction> stateMachine;

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateBase());
        }

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        { 
            SwitchState(BossAction.INIT);
        }

        #endregion

        #region STATE MACHINE

        private void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }

        #endregion
    }
}
