using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EDGEE.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter(params object[] objs) // poderia ser "object o = null" ou "params object[] objs"
        {
            Debug.Log("OnStateEnter");
        }
        public virtual void OnStateStay()
        {
            Debug.Log("OnStateStay");
        }
        public virtual void OnStateExit()
        {
            Debug.Log("OnStateExit");
        }

    }
}