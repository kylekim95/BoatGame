using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        protected Dictionary<string, int> states;
        protected List<Action> stateActions;
        int curState;
        public bool coolDown = false;

        protected virtual void Awake()
        {
            states = new Dictionary<string, int>();
            stateActions = new List<Action>();

            states.Add("Idle", 0);
            stateActions.Add(StateAction_Idle);
            curState = 0;
        }
        private void Update()
        {
            stateActions[curState].Invoke();
        }

        void StateAction_Idle()
        {
            
        }
        public void ChangeState(string to)
        {
            if (states.ContainsKey(to) && !coolDown)
                curState = states[to];
        }

        protected IEnumerator CoolDownTimer(float time)
        {
            coolDown = true;
            if(time > 0)
                yield return new WaitForSeconds(time);
            coolDown = false;
            ChangeState("Idle");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        }
    }
}