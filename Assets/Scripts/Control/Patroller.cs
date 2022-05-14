using UnityEngine;
using UnityEngine.AI;

namespace App.Control
{
    public class Patroller : MonoBehaviour
    {
        public enum PatrolState { WALKING, ALERT }
        public Transform path = null;
        NavMeshAgent agent = null;
        int index = 0;
        bool isPatrolling = false;
        public Transform target;
        PatrolState state = PatrolState.WALKING;
        float timer = 0, alertTime = 4f;
        Animator animator = null;

        bool OnWaypoint(int index)
        {
            if (Vector3.Distance(transform.position, path.GetChild(index).position) <= agent.stoppingDistance)
                return true;
            return false;
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            index = Random.Range(0, path.childCount);
        }

        void Update()
        {
            if (isPatrolling)
            {
                switch (state)
                {
                    case PatrolState.WALKING:
                        agent.isStopped = false;
                        target = path.GetChild(index);
                        GetComponent<MoveEntity>().Seek(path.GetChild(index).transform.position);
                        if (OnWaypoint(index))
                        {
                            state = PatrolState.ALERT;
                            animator.SetBool("isAlert", true);
                        }
                        break;
                    case PatrolState.ALERT:
                        agent.isStopped = true;
                        timer += Time.deltaTime;
                        if (timer >= alertTime)
                        {
                            timer = 0;
                            index = Random.Range(0, path.childCount);
                            state = PatrolState.WALKING;
                            target = null;
                            animator.SetBool("isAlert", false);
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

        public void StartPatrol()
        {
            agent.isStopped = false;
            agent.speed = GetComponent<MoveEntity>().abilityConfig.walkSpeed;
            isPatrolling = true;
        }

        public void ExitPatrol()
        {
            animator.SetBool("isAlert", false);
            agent.speed = GetComponent<MoveEntity>().abilityConfig.runSpeed;
            isPatrolling = false;
        }
    }
}
