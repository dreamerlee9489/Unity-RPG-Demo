using Control.FSM;
using Control.MSG;
using Manager;
using UnityEngine;

namespace Control
{
    [RequireComponent(typeof(Entity))]
    public class StateController : MonoBehaviour
    {
        private State currentState { get; set; }
        private State previousState { get; set; }
        private State globalState { get; set; }

        void Start()
        {
            currentState = new Idle(GetComponent<Entity>(), GameManager.Instance.player);
        }

        void Update()
        {
            if (!GetComponent<Entity>().isDead)
            {
                if (globalState != null)
                    globalState.Execute();
                if (currentState != null)
                    currentState.Execute();
            }
            else
            {
                if (globalState != null)
                {
                    globalState.Exit();
                    globalState = null;
                }

                if (currentState != null)
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