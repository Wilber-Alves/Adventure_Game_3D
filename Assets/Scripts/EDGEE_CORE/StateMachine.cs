using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace EDGEE.StateMachine
{

    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> dictionaryStates;

        private StateBase _currentState;

        public float timeToStartGame = 1.0f;

        public StateBase CurrentState
        {
            get
            {
                return _currentState;
            }

        }

        public void Init()
        {
            dictionaryStates = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            dictionaryStates.Add(typeEnum, state);
        }

        public void SwitchState(T state, params object[] objs) // poderia ser "object o = null" ou "params object[] objs"
        {
            if (_currentState != null) _currentState.OnStateExit();
            _currentState = dictionaryStates[state];
            _currentState.OnStateEnter(objs); // aqui poderia se "o" ou "objs" dependendo se uso objeto nulo ou a tecnica de params, respectivamente
        }

        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.OnStateStay();
            }
        }
    }
}
