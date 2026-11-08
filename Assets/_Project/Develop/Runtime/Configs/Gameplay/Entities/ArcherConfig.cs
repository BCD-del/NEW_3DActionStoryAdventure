using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewArcherConfig", fileName = "ArcherConfig")]
    public class ArcherConfig : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; private set; } = "Entities/Archer";

        [field: Header("Movement")]
        [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 4.5f;
        [field: SerializeField, Min(0)] public float RotationSpeed { get; private set; } = 540;

        [field: Header("Health")]
        [field: SerializeField, Min(0)] public float MaxHealth { get; private set; } = 70;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = 2;

        [field: Header("Combat (дальний бой)")]
        [field: SerializeField, Min(0)] public float AttackRange { get; private set; } = 8f;
        [field: SerializeField, Min(0)] public float SpotRangeDistance { get; private set; } = 12f; // дистанция, на которой лучник замечает игрока
        [field: SerializeField, Min(0)] public float AttackProcessTime { get; private set; } = 1.2f;
        [field: SerializeField, Min(0)] public float AttackDelayTime { get; private set; } = 0.6f;
        [field: SerializeField, Min(0)] public float AttackCooldown { get; private set; } = 1.5f;
        [field: SerializeField, Min(0)] public float ArrowDamage { get; private set; } = 15;
       

    }
}
