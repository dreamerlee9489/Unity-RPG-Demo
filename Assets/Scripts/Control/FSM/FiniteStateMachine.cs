
using UnityEngine;

namespace App.Control.FSM
{
    [RequireComponent(typeof(MoveEntity), typeof(CombatEntity))]
    public class FiniteStateMachine : MonoBehaviour
    {
        Transform player = null;
        public State currentState { get; set; }
        public State previousState { get; set; }
        public State globalState { get; set; }

        void Start()
        {
            player = GameManager.Instance.entities["Player"].transform;
            currentState = new Idle(this, player);
        }

        void Update()
        {
            if (!GetComponent<CombatEntity>().isDead)
            {
                if (globalState != null)
                    globalState.Execute();
                if (currentState != null)
                    currentState.Execute();
            }
            else
            {
                if(globalState != null)
                {
                    globalState.Exit();
                    globalState = null;
                }
                if(currentState != null)
                {
                    currentState.Exit();
                    currentState = null;
                }
            }
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
