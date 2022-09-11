using System.Collections.Generic;
using Control.CMD;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Manager;

namespace Control
{
    [RequireComponent(typeof(Entity))]
    public class PlayerController : MonoBehaviour
    {
        private RaycastHit _hit;
        private Animator _animator = null;
        private NavMeshAgent _agent = null;
        private Entity _entity = null;
        private readonly List<Command> _commands = new List<Command>();
        public Transform bag = null;
        public Transform skills = null;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _entity = GetComponent<Entity>();
            _commands.Add(new CombatCommand(_entity));
            _commands.Add(new DialogueCommand(UIManager.Instance));
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            UIManager.Instance.hudPanel.UpdatePanel();
            UIManager.Instance.attributePanel.UpdatePanel();
        }

        private void Update()
        {
            if (!_entity.isDead)
            {
                if (_entity.target != null)
                    _entity.ExecuteAction(_entity.target);
                if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
                    CancelCommand();
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit))
                    {
                        _entity.CancelAction();
                        UIManager.Instance.target = null;
                        switch (_hit.collider.tag)
                        {
                            case "Terrain":
                                ExecuteCommand(0, _hit.point);
                                break;
                            case "Enemy":
                                ExecuteCommand(0, _hit.transform);
                                _agent.stoppingDistance = 1f + 0.3f * _entity.target.localScale.x;
                                _entity.sqrAttackRadius = Mathf.Pow(_agent.stoppingDistance, 2);
                                break;
                            case "NPC":
                                ExecuteCommand(1, _hit.transform);
                                _agent.stoppingDistance = 1f;
                                break;
                            case "DropItem":
                                ExecuteCommand(0, _hit.transform);
                                _agent.stoppingDistance = 1f;
                                break;
                            case "Portal":
                                ExecuteCommand(0, _hit.transform.position + new Vector3(-2.5f, 0, 0));
                                _agent.stoppingDistance = 1f;
                                break;
                        }
                    }
                }
            }
        }

        void ExecuteCommand(int index, Vector3 point) => _commands[index].Execute(point);
        void ExecuteCommand(int index, Transform target) => _commands[index].Execute(target);

        public void CancelCommand()
        {
            foreach (var command in _commands)
                command.Cancel();
        }
    }
}