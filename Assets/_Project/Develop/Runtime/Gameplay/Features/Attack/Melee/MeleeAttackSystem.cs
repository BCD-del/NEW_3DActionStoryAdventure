using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Attack.Melee
{
    /// <summary>
    /// Ближний бой без пуль и префабов: в момент удара (конец задержки атаки)
    /// наносит урон всем вражеским сущностям впереди персонажа.
    /// Работает и для героя, и для врагов — им не нужен слой Characters,
    /// потому что цель ищется через CollidersRegistryService.
    /// </summary>
    public class MeleeAttackSystem : IInitializableSystem, IDisposableSystem
    {
        private const int MaxHits = 32;

        private readonly CollidersRegistryService _collidersRegistry;

        private readonly float _radius;
        private readonly float _forwardOffset;
        private readonly float _heightOffset;

        private Entity _entity;
        private Transform _transform;
        private ReactiveVariable<float> _damage;

        private IDisposable _attackDelayEndDisposable;

        private readonly Collider[] _hits = new Collider[MaxHits];
        private readonly List<Entity> _damaged = new(MaxHits);

        public MeleeAttackSystem(
            CollidersRegistryService collidersRegistry,
            float radius = 1.8f,
            float forwardOffset = 1f,
            float heightOffset = 0.9f)
        {
            _collidersRegistry = collidersRegistry;
            _radius = radius;
            _forwardOffset = forwardOffset;
            _heightOffset = heightOffset;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _transform = entity.Transform;
            _damage = entity.InstantAttackDamage;

            _attackDelayEndDisposable = entity.AttackDelayEndEvent.Subscribe(OnAttackDelayEnd);
        }

        private void OnAttackDelayEnd()
        {
            if (_transform == null)
                return;

            Vector3 hitCenter = _transform.position
                + _transform.forward * _forwardOffset
                + Vector3.up * _heightOffset;

            int hitCount = Physics.OverlapSphereNonAlloc(
                hitCenter,
                _radius,
                _hits,
                ~0,
                QueryTriggerInteraction.Ignore);

            _damaged.Clear();

            int damagedCount = 0;

            for (int i = 0; i < hitCount; i++)
            {
                Entity target = _collidersRegistry.GetBy(_hits[i]);

                if (target == null || target == _entity || _damaged.Contains(target))
                    continue;

                _damaged.Add(target);

                if (EntitiesHelper.TryTakeDamageFrom(_entity, target, _damage.Value))
                    damagedCount++;
            }

            if (damagedCount > 0)
                Debug.Log($"Удар попал! Задето целей: {damagedCount}, урон: {_damage.Value:0.#}");
        }

        public void OnDispose()
        {
            _attackDelayEndDisposable.Dispose();
        }
    }
}
