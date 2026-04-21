using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public enum ExempleEnum
    {
       STATE_ONE,
       STATE_TWO,
       STATE_THREE,
    }

    public StateMachine<ExempleEnum> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<ExempleEnum>();
        stateMachine.Init();
        stateMachine.RegisterStates(ExempleEnum.STATE_ONE, new StateBase());
        stateMachine.RegisterStates(ExempleEnum.STATE_TWO, new StateBase());
        stateMachine.RegisterStates(ExempleEnum.STATE_THREE, new StateBase());
    }

}
