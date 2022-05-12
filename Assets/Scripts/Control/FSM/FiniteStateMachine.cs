using UnityEngine;
using UnityEngine.AI;

namespace Game.Control.FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        Animator animator = null;
        NavMeshAgent agent = null;
        Transform target = null;
        public State currentState { get; set; }
        public State previousState { get; set; }
        public State globalState { get; set; }

        public void RevertPreviousState() => ChangeState(previousState);
        public bool IsInState(State state) => currentState.GetType() == state.GetType();
        public void ChangeState(State newState)
        {
            previousState = currentState;
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }

        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
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
    }
}
