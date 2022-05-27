using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using App.Manager;

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
        List<Command> commands = new List<Command>();
        public Transform bag = null;
        public Transform skills = null;

        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            moveEntity = GetComponent<MoveEntity>();
            combatEntity = GetComponent<CombatEntity>();
            commands.Add(new MoveCommand(moveEntity));
            commands.Add(new CombatCommand(combatEntity));
            commands.Add(new DialogueCommand(UIManager.Instance));
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if (!combatEntity.isDead)
            {
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                    {
                        combatEntity.CancelAction();
                        UIManager.Instance.target = null;
                        switch (hit.collider.tag)
                        {
                            case "Terrain":
                                ExecuteCommand(0, hit.point);
                                break;
                            case "Enemy":
                                ExecuteCommand(1, hit.transform);
                                agent.stoppingDistance = combatEntity.entityConfig.stopDistance + combatEntity.target.GetComponent<CombatEntity>().entityConfig.stopDistance;
                                combatEntity.sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
                                break;
                            case "NPC":
                                ExecuteCommand(2, hit.transform);
                                agent.stoppingDistance = 1.5f;
                                break;
                            case "Drop":
                                ExecuteCommand(1, hit.transform);
                                agent.stoppingDistance = 1.5f;
                                break;
                            case "Portal":
                                ExecuteCommand(0, hit.transform.position);
                                break;
                        }
                    }
                }
                if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
                    CancelCommand();
                if (combatEntity.target != null)
                {
                    if(!combatEntity.immovable)
                        combatEntity.ExecuteAction(combatEntity.target);
                    else
                        combatEntity.CancelAction();
                }                 
            }
        }

        void ExecuteCommand(int index, Vector3 point) => commands[index].Execute(point);
        void ExecuteCommand(int index, Transform target) => commands[index].Execute(target);
        void CancelCommand()
        {
            foreach (var command in commands)
                command.Cancel();
        }
    }
}