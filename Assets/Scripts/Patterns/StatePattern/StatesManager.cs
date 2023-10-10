using System.Collections.Generic;
using UnityEngine;

namespace Gabevlogd.Patterns
{

    /// <summary>
    /// State manager to handle basic states
    /// </summary>
    /// <typeparam name="TStateIDType">The type of the state base ID</typeparam>
    public abstract class StatesManager<TStateIDType> 
    {
        public Dictionary<TStateIDType, State<TStateIDType>> AllStates;
        public State<TStateIDType> CurrentState;
        public State<TStateIDType> PreviousState;

        

        protected virtual void InitStatesManager()
        {
            if (AllStates == null) AllStates = new Dictionary<TStateIDType, State<TStateIDType>>();
            InitStates();
        }

        /// <summary>
        /// Loads all the states in the dictionary 
        /// </summary>
        protected abstract void InitStates();

        /// <summary>
        /// Changes the current state to the passed state type (only if the current state is not already the passed state type)
        /// </summary>
        /// <param name="stateIDType"></param>
        public void ChangeState(TStateIDType stateIDType)
        {
            if (CurrentState == AllStates[stateIDType]) return;

            PreviousState = CurrentState;
            CurrentState.OnExit();
            CurrentState = AllStates[stateIDType];
            CurrentState.OnEnter();
        }
    }
}


