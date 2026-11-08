using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class StartAttackSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _startAttackRequest;
        private ReactiveEvent _startAttackEvent;
        private ReactiveVariable<bool> _inAttackProcess;
        private ReactiveVariable<bool> _inAttackCooldown;
        private ReactiveVariable<bool> _isDead;
        private ICompositeCondition _canStartAttack;

        private IDisposable _attackRequestDispose;

        public void OnInit(Entity entity)
        {
            _startAttackRequest = entity.StartAttackRequest;
            _startAttackEvent = entity.StartAttackEvent;
            _inAttackProcess = entity.InAttackProcess;
            _canStartAttack = entity.CanStartAttack;

            entity.TryGetInAttackCooldown(out _inAttackCooldown);
            entity.TryGetIsDead(out _isDead);

            _attackRequestDispose = _startAttackRequest.Subscribe(OnAttackRequest);
        }

        private void OnAttackRequest()
        {
            if (_canStartAttack.Evaluate())
            {
                _inAttackProcess.Value = true;
                _startAttackEvent.Invoke();
                Debug.Log("Старт атаки");
            }
            else
            {
                Debug.Log($"Не могу атаковать! мёртв: {_isDead?.Value}, в процессе атаки: {_inAttackProcess.Value}, кулдаун: {_inAttackCooldown?.Value}");
            }
        }

        public void OnDispose()
        {
            _attackRequestDispose.Dispose();
        }

       
    }
}
