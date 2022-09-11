using UnityEngine;
using UnityEngine.AI;
using Control.BT;
using Control.BT.Composite;
using Control.BT.Decorator;
using Manager;

namespace Control
{
    [RequireComponent(typeof(Entity))]
    public class BehaviorController : MonoBehaviour
    {
        private float _wanderTimer = 6f;
        private Animator _animator = null;
        private NavMeshAgent _agent = null;
        private Transform _player = null;
        private Entity _entity = null;
        private readonly Selector _root = new Selector();

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _entity = GetComponent<Entity>();
        }

        private void Start()
        {
            _player = GameManager.Instance.player.transform;
            Sequence retreat = new Sequence();
            Parallel wander = new Parallel();
            Parallel chase = new Parallel();
            Condition canSeePlayer = new Condition(() =>
            {
                if (_entity.CanSee(_player))
                {
                    _agent.speed = _entity.entityConfig.runSpeed * _entity.entityConfig.runFactor * _entity.speedRate;
                    return true;
                }

                _agent.speed = _entity.entityConfig.walkSpeed * _entity.entityConfig.walkFactor;
                return false;
            });
            _root.AddChildren(retreat, wander, chase);
            retreat.AddChildren(new Condition(() => false), new Action(() =>
            {
                if (_entity.Flee(_player.position))
                    return Status.Success;
                return Status.Running;
            }), new Action(() => Status.Success));
            wander.AddChildren(new UntilSuccess(canSeePlayer), new Action(() =>
            {
                _wanderTimer += Time.deltaTime;
                if (_wanderTimer >= 6f)
                {
                    _entity.Wander();
                    _wanderTimer = 0;
                }

                return Status.Running;
            }));
            chase.AddChildren(new UntilFailure(canSeePlayer), new Action(() =>
            {
                _entity.ExecuteAction(_player);
                return Status.Running;
            }));
        }

        private void Update()
        {
            if (!_entity.isDead)
                _root.Execute();
        }
    }
}