using UnityEngine;

namespace Game.Control
{
    public class PlayerController : MonoBehaviour
    {
        MoveEntity moveEntity = null;
        RaycastHit hit;
        Command command;

        void ExecuteCommand(Command command, Vector3 position)
        {
            this.command = command;
            command.Execute(position);
        }

        void CancelCommand()
        {
        }

        void Awake()
        {
            moveEntity = GetComponent<MoveEntity>();
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
                        ExecuteCommand(new MoveCommand(moveEntity), hit.point);
                        break;
                    }
                }
            }
        }
    }
}