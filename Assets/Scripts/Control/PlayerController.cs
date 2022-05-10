using UnityEngine;
using UnityEngine.AI;

namespace Game.Control
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

        void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            moveEntity = GetComponent<MoveEntity>();
            combatEntity = GetComponent<CombatEntity>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    switch (hit.collider.tag)
                    {
                        case "Terrain":
                            ExecuteCommand(new MoveCommand(moveEntity), hit);
                            break;
                        case "Enemy":
                            ExecuteCommand(new CombatCommand(combatEntity), hit);
                            break;
                    }
                }
            }
            if (combatEntity.target != null)
            {
                if (combatEntity.CanAttack(combatEntity.target))
                {
                    animator.SetBool("attack", true);
                }
                else
                {
                    agent.destination = combatEntity.target.position;
                    transform.LookAt(combatEntity.target);
                    animator.SetBool("attack", false);
                }
            }
        }
    }
}