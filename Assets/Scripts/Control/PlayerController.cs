using App.Manager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace App.Control
{
    [RequireComponent(typeof(MoveEntity), typeof(CombatEntity))]
    public class PlayerController : MonoBehaviour
    {
        RaycastHit hit;
        Animator animator = null;
        NavMeshAgent agent = null;
        MoveEntity moveEntity = null;
        CombatEntity combatEntity = null;
        Command command = null;

        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            moveEntity = GetComponent<MoveEntity>();
            combatEntity = GetComponent<CombatEntity>();
        }

        void Update()
        {
            if (!combatEntity.isDead)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                        {
                            switch (hit.collider.tag)
                            {
                                case "Terrain":
                                    ExecuteCommand(new MoveCommand(moveEntity), hit.point);
                                    break;
                                case "Enemy":
                                    ExecuteCommand(new CombatCommand(combatEntity), hit.transform);
                                    agent.stoppingDistance = moveEntity.abilityConfig.stopDistance + combatEntity.target.GetComponent<MoveEntity>().abilityConfig.stopDistance;
                                    combatEntity.sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
                                    break;
                                case "NPC":
                                    ExecuteCommand(new DialogueCommand(GameManager.Instance.canvas), hit.transform);
                                    break;
                            }
                        }
                    }
                }
                if (Input.GetMouseButtonDown(1))
                    CancelCommand();
                if (combatEntity.target != null)
                    combatEntity.ExecuteAction(combatEntity.target);
            }
        }

        void ExecuteCommand(Command command, Vector3 point)
        {
            this.command = command;
            command.Execute(point);
        }

        void ExecuteCommand(Command command, Transform target)
        {
            this.command = command;
            command.Execute(target);
        }

        void CancelCommand()
        {
            command?.Cancel();
        }
    }
}