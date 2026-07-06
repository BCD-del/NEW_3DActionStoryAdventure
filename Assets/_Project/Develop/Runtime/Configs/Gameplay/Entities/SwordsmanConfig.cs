using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewSwordsmanConfig", fileName = "SwordsmanConfig")]
    public class SwordsmanConfig : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; private set; } = "Entities/Swordsman";

        [field: Header("Movement")]
        [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 6;
        [field: SerializeField, Min(0)] public float RotationSpeed { get; private set; } = 720;

        [field: Header("Health")]
        [field: SerializeField, Min(0)] public float MaxHealth { get; private set; } = 120;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = 2;

        [field: Header("Combat (ближний бой)")]
        [field: SerializeField, Min(0)] public float AttackRange { get; private set; } = 2f;
        [field: SerializeField, Min(0)] public float AttackProcessTime { get; private set; } = 1f;
        [field: SerializeField, Min(0)] public float AttackDelayTime { get; private set; } = 0.35f;
        [field: SerializeField, Min(0)] public float AttackCooldown { get; private set; } = 0.5f;
        [field: SerializeField, Min(0)] public float AttackDamage { get; private set; } = 25;
       
    }
}
