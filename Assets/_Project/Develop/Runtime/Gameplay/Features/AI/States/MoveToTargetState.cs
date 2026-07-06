using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    // Двигает сущность к CurrentTarget, пока дистанция больше stopDistance.
    // Как только враг подошёл достаточно близко (обычно это - дистанция атаки) - останавливается.
    public class MoveToTargetState : State, IUpdatableState
    {
        private readonly ReactiveVariable<Vector3> _moveDirection;
        private readonly ReactiveVariable<bool> _isMoving;
        private readonly ReactiveVariable<Entity> _currentTarget;
        private readonly Transform _transform;
        private readonly float _stopDistance;

        public MoveToTargetState(Entity entity, float stopDistance)
        {
            _moveDirection = entity.MoveDirection;
            _isMoving = entity.IsMoving;
            _currentTarget = entity.CurrentTarget;
            _transform = entity.Transform;
            _stopDistance = stopDistance;
        }

        public override void Exit()
        {
            base.Exit();

            _isMoving.Value = false;
        }

        public void Update(float deltaTime)
        {
            Entity target = _currentTarget.Value;

            if (target == null)
            {
                _isMoving.Value = false;
                return;
            }

            Vector3 toTarget = target.Transform.position - _transform.position;
            toTarget.y = 0;

            if (toTarget.magnitude <= _stopDistance)
            {
                _isMoving.Value = false;
                return;
            }

            _moveDirection.Value = toTarget.normalized;
            _isMoving.Value = true;
        }
    }
}
