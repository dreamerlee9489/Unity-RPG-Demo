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
        Command[] commands = null;

        void Start()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            moveEntity = GetComponent<MoveEntity>();
            combatEntity = GetComponent<CombatEntity>();
            commands = new Command[3] { new MoveCommand(moveEntity), new CombatCommand(combatEntity), new DialogueCommand(GameManager.Instance.canvas) };
        }

        void Update()
        {
            if (!combatEntity.isDead)
            {
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                    {
                        switch (hit.collider.tag)
                        {
                            case "Terrain":
                                ExecuteCommand(0, hit.point);
                                break;
                            case "Enemy":
                                ExecuteCommand(1, hit.transform);
                                agent.stoppingDistance = moveEntity.abilityConfig.stopDistance + combatEntity.target.GetComponent<MoveEntity>().abilityConfig.stopDistance;
                                combatEntity.sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
                                break;
                            case "NPC":
                                ExecuteCommand(2, hit.transform);
                                break;
                        }
                    }
                }
                if (Input.GetMouseButtonDown(1))
                    CancelCommand();
                if (combatEntity.target != null)
                    combatEntity.ExecuteAction(combatEntity.target);
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