using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.SO;

namespace Game.Control
{
    public class MoveEntity : MonoBehaviour, IReceiver
    {
        float sqrFleeRadius = 36f;
        Animator animator = null;
        NavMeshAgent agent = null;
        public AbilityConfig abilityConfig = null;

        public void ExecuteAction(RaycastHit hit)
        {
            if (GetComponent<CombatEntity>().target != null)
                GetComponent<CombatEntity>().CancelAction();
            agent.destination = hit.point;
        }

        public void CancelAction()
        {
            agent.destination = transform.position;
        }

        public float Seek(Vector3 position)
        {
            agent.autoBraking = false;
            agent.isStopped = false;
            agent.speed = abilityConfig.runSpeed * abilityConfig.speedFactor;
            agent.destination = position;
            return Vector3.Distance(position, agent.destination);
        }

        public bool Flee(Vector3 position)
        {
            Vector3 direction = transform.position - position;
            if (direction.sqrMagnitude <= Mathf.Pow(abilityConfig.fleeRadius, 2))
            {
                agent.autoBraking = false;
                agent.isStopped = false;
                agent.speed = abilityConfig.runSpeed * abilityConfig.speedFactor;
                agent.destination = direction.normalized * abilityConfig.fleeRadius;
            }
            return direction.sqrMagnitude > sqrFleeRadius;
        }

        public void Arrive(Vector3 position)
        {
            agent.autoBraking = true;
            agent.isStopped = false;
            agent.speed = abilityConfig.runSpeed * abilityConfig.speedFactor;
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

        public void Wander(float radius = 6f)
        {
            Vector3 position = transform.position + Random.insideUnitSphere * radius;
            agent.autoBraking = false;
            agent.isStopped = false;
            agent.speed = abilityConfig.walkSpeed;
            NavMeshHit hit;
            for (int i = 0; i < 10; i++)
            {
                if (NavMesh.SamplePosition(position, out hit, 1, NavMesh.AllAreas))
                {
                    agent.destination = hit.position;
                    return;
                }
            }
            agent.destination = transform.position;
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

        private Vector3 GetHidePosition(NavMeshObstacle obstacle, NavMeshAgent target, float distanceFromBoundary = 3f)
        {
            float distAway = obstacle.radius + distanceFromBoundary;
            Vector3 toObstacle = (obstacle.transform.position - target.transform.position).normalized;
            return obstacle.transform.position + toObstacle * distAway;
        }

        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            sqrFleeRadius = Mathf.Pow(abilityConfig.fleeRadius, 2);
            agent.isStopped = false;
            agent.autoBraking = false;
            agent.speed = abilityConfig.runSpeed * abilityConfig.speedFactor;
        }

        void Update()
        {
            animator.SetFloat("moveSpeed", transform.InverseTransformVector(agent.velocity).z);
        }
    }
}
