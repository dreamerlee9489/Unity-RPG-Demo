using UnityEngine;
using UnityEngine.AI;

namespace App.Control.FSM
{
    [RequireComponent(typeof(MoveEntity), typeof(CombatEntity))]
    public class FiniteStateMachine : MonoBehaviour
    {
        Transform target = null;
        public State currentState { get; set; }
        public State previousState { get; set; }
        public State globalState { get; set; }

        void Awake()
        {
            target = GameObject.FindWithTag("Player").transform;
            currentState = new Patrol(this, target);
        }

        void Update()
        {
            if (globalState != null)
                globalState.Execute();
            if (currentState != null)
                currentState.Execute();
        }

        public void RevertPreviousState() => ChangeState(previousState);

        public bool IsInState(State state) => currentState.GetType() == state.GetType();

        public void ChangeState(State newState)
        {
            previousState = currentState;
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public bool HandleMessage(Telegram telegram)
        {
            if (currentState != null && currentState.OnMessage(telegram))
                return true;
            if (globalState != null && globalState.OnMessage(telegram))
                return true;
            return false;
        }
    }
}
