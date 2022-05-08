using UnityEngine;
using Game.SO;

namespace Game.Control
{
    [RequireComponent(typeof(MoveEntity), typeof(CombatEntity))]
    public class PlayerController : MonoBehaviour
    {
        MoveEntity moveEntity = null;
        CombatEntity combatEntity = null;
        Command command = null;
        RaycastHit hit;

        void ExecuteCommand(Command command, RaycastHit hit)
        {
            this.command = command;
            command.Execute(hit);
        }

        void Awake()
        {
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
        }
    }
}