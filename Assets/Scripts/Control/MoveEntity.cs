using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using App.SO;

namespace App.Control
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveEntity : MonoBehaviour, ICmdReceiver
    {
        float sqrFleeRadius = 36f;
        Vector3 initPos;
        Animator animator = null;
        NavMeshAgent agent = null;
        CombatEntity combatEntity = null;
        public AbilityConfig abilityConfig = null;

        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            combatEntity = GetComponent<CombatEntity>();
            animator.applyRootMotion = false;
            agent.autoBraking = false;
            agent.angularSpeed = 4800;
            agent.acceleration = 80;
            agent.speed = abilityConfig.runSpeed * abilityConfig.runFactor;
            initPos = transform.position;
            sqrFleeRadius = Mathf.Pow(abilityConfig.fleeRadius, 2);
        }

        void Update()
        {
            animator.SetFloat("moveSpeed", transform.InverseTransformVector(agent.velocity).z);
        }

        Vector3 GetHidePosition(NavMeshObstacle obstacle, NavMeshAgent target, float distanceFromBoundary = 3f)
        {
            float distAway = obstacle.radius + distanceFromBoundary;
            Vector3 toObstacle = (obstacle.transform.position - target.transform.position).normalized;
            return obstacle.transform.position + toObstacle * distAway;
        }
        
        public void ExecuteAction(Transform target) {}
        public void ExecuteAction(Vector3 point)
        {
            if (combatEntity.target != null)
                combatEntity.CancelAction();
            agent.isStopped = false;
            agent.destination = point;
            agent.stoppingDistance = abilityConfig.stopDistance;
        }

        public void CancelAction()
        {
            agent.isStopped = true;
        }

        public float Seek(Vector3 position)
        {
            agent.autoBraking = false;
            agent.destination = position;
            return Vector3.Distance(position, agent.destination);
        }

        public bool Flee(Vector3 position)
        {
            Vector3 direction = transform.position - position;
            if (direction.sqrMagnitude <= Mathf.Pow(abilityConfig.fleeRadius, 2))
            {
                agent.autoBraking = false;
                agent.destination = direction.normalized * abilityConfig.fleeRadius;
            }
            return direction.sqrMagnitude > sqrFleeRadius;
        }

        public void Arrive(Vector3 position)
        {
            agent.autoBraking = true;
            agent.destination = position;
        }

        public void Pursuit(NavMeshAgent evader)
        {
            Vector3 toEvader = evader.transform.position - transform.position;
            float relativeHeading = Vector3.Dot(transform.forward, evader.transform.forward);
            if (Vector3.Dot(transform.forward, toEvader) > 0 && (relativeHeading < -0.95f))
            {
                Seek(evader.transform.position);
                return;
            }
            float lookAheadTime = toEvader.magnitude / (agent.speed + evader.speed);
            Seek(evader.transform.position + evader.velocity * lookAheadTime);
        }

        public void Evade(NavMeshAgent pursuer)
        {
            Vector3 toPursuer = pursuer.transform.position - transform.position;
            float lookAheadTime = toPursuer.magnitude / (agent.speed + pursuer.speed);
            Flee(pursuer.transform.position + pursuer.velocity * lookAheadTime);
        }

        public void Wander(float radius = 10f)
        {
            Vector3 position = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
            position = position.normalized * radius * Random.Range(0f, 1f);
            agent.destination = initPos + position;
        }

        public void Interpose(NavMeshAgent agentA, NavMeshAgent agentB)
        {
            Vector3 midPoint = (agentA.transform.position + agentB.transform.position) / 2.0f;
            float timeToReachMidPoint = Vector3.Distance(transform.position, midPoint) / agent.speed;
            Vector3 posA = agentA.transform.position + agentA.velocity * timeToReachMidPoint;
            Vector3 posB = agentB.transform.position + agentB.velocity * timeToReachMidPoint;
            midPoint = (posA + posB) / 2.0f;
            Arrive(midPoint);
        }

        public void Hide(NavMeshAgent hunter, List<NavMeshObstacle> obstacles)
        {
            float distToClosest = float.MaxValue;
            Vector3 bestHideSpot = Vector3.zero;
            NavMeshObstacle closest = null;
            foreach (NavMeshObstacle obstacle in obstacles)
            {
                Vector3 hideSpot = GetHidePosition(obstacle, hunter);
                float dist = Vector3.Distance(hideSpot, transform.position);
                if (dist < distToClosest)
                {
                    distToClosest = dist;
                    bestHideSpot = hideSpot;
                    closest = obstacle;
                }
            }
            if (distToClosest == float.MaxValue)
            {
                Evade(hunter);
                return;
            }
            Arrive(bestHideSpot);
        }

        public void OffsetPursuit(NavMeshAgent leader, Vector3 offset)
        {
            Vector3 worldOffsetPos = leader.transform.TransformVector(offset);
            Vector3 toOffset = worldOffsetPos - transform.position;
            float lookAheadTime = toOffset.magnitude / (agent.speed + leader.speed);
            Arrive(worldOffsetPos + leader.velocity * lookAheadTime);
        }
    }
}
