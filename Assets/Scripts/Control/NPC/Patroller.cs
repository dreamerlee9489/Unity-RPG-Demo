using UnityEngine;
using UnityEngine.AI;

namespace Control.NPC
{
    public class Patroller : MonoBehaviour
    {
        private enum PatrolState
        {
            Walking,
            Alert
        }

        public Transform path = null;
        private NavMeshAgent _agent = null;
        private int _index = 0;
        private bool _isPatrolling = false;
        public Transform target;
        private PatrolState _state = PatrolState.Walking;
        private float _timer = 0;
        private Animator _animator = null;
        private static readonly int isAlert = Animator.StringToHash("isAlert");
        private const float AlertTime = 4f;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _index = Random.Range(0, path.childCount);
        }

        private void Update()
        {
            if (_isPatrolling)
            {
                switch (_state)
                {
                    case PatrolState.Walking:
                        _agent.isStopped = false;
                        target = path.GetChild(_index);
                        GetComponent<Entity>().Seek(path.GetChild(_index).transform.position);
                        if (OnWaypoint(_index))
                        {
                            _state = PatrolState.Alert;
                            _animator.SetBool(isAlert, true);
                        }

                        break;
                    case PatrolState.Alert:
                        _agent.isStopped = true;
                        _timer += Time.deltaTime;
                        if (_timer >= AlertTime)
                        {
                            _timer = 0;
                            _index = Random.Range(0, path.childCount);
                            _state = PatrolState.Walking;
                            target = null;
                            _animator.SetBool(isAlert, false);
                        }

                        break;
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < path.childCount; i++)
            {
                Gizmos.DrawSphere(path.GetChild(i).position, 0.3f);
                Gizmos.DrawLine(path.GetChild(i).position, path.GetChild((i + 1) % path.childCount).position);
            }
        }

        bool OnWaypoint(int index)
        {
            if (Vector3.Distance(transform.position, path.GetChild(index).position) <= _agent.stoppingDistance)
                return true;
            return false;
        }

        public void StartPatrol()
        {
            _agent.isStopped = false;
            _agent.speed = GetComponent<Entity>().entityConfig.walkSpeed;
            _isPatrolling = true;
        }

        public void ExitPatrol()
        {
            _animator.SetBool(isAlert, false);
            _agent.speed = GetComponent<Entity>().entityConfig.runSpeed;
            _isPatrolling = false;
        }
    }
}