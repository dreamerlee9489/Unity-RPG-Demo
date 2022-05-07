using UnityEngine;

namespace Game.Control
{
    [RequireComponent(typeof(MoveEntity), typeof(CombatEntity))]
    public class EnemyController : MonoBehaviour
    {
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
            command.Cancel();
        }

        void Awake()
        {
            moveEntity = GetComponent<MoveEntity>();
            combatEntity = GetComponent<CombatEntity>();
        }
    }
}
