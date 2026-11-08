using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Sensors
{
    public class BodyColliderRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private CapsuleCollider _body;

        public override void Register(Entity entity)
        {
            if (_body == null)
            {
                Debug.LogWarning($"{name}: у {nameof(BodyColliderRegistrator)} не назначен CapsuleCollider — контактный урон тела отключён", this);
                return;
            }

            entity.AddBodyCollider(_body);
        }
    }
}
