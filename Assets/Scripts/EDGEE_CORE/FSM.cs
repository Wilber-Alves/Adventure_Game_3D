using EDGEE.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
public class FSM : MonoBehaviour
{
    public enum ExempleEnum
    {
       RUN,
       STOP,
       JUMP,
    }

    public StateMachine<ExempleEnum> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<ExempleEnum>();
        stateMachine.Init();
        stateMachine.RegisterStates(ExempleEnum.RUN, new StateBase());
        stateMachine.RegisterStates(ExempleEnum.STOP, new StateBase());
        stateMachine.RegisterStates(ExempleEnum.JUMP, new StateBase());
    }

}
