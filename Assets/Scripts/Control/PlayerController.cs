using App.Manager;
using App.UI;
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

        void ExecuteCommand(Command command, RaycastHit hit)
        {
            this.command = command;
            command.Execute(hit);
        }

        void CancelCommand()
        {
            command?.Cancel();
        }

        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            moveEntity = GetComponent<MoveEntity>();
            combatEntity = GetComponent<CombatEntity>();
        }

        void Update()
        {
            if (combatEntity.target != null)
            {
                transform.LookAt(combatEntity.target);
                if (combatEntity.CanAttack(combatEntity.target))
                {
                    animator.SetBool("attack", true);
                }
                else
                {
                    agent.destination = combatEntity.target.position;
                    animator.SetBool("attack", false);
                }
            }
            if(Input.GetMouseButtonDown(1))
                CancelCommand();
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                }
                else if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    switch (hit.collider.tag)
                    {
                        case "Terrain":
                            ExecuteCommand(new MoveCommand(moveEntity), hit);
                            break;
                        case "Enemy":
                            ExecuteCommand(new CombatCommand(combatEntity), hit);
                            break;
                        case "NPC":
                            ExecuteCommand(new DialogueCommand(GameManager.Instance.canvas), hit);
                            break;
                    }
                }
            }
        }
    }
}