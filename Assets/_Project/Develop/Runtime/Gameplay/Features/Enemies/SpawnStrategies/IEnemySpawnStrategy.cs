using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Enemies.SpawnStrategies
{
    // Одна стратегия = один вид врага.
    // EnemiesFactory ничего не знает о конкретных видах врагов, только об этом интерфейсе.
    public interface IEnemySpawnStrategy
    {
        bool CanHandle(EntityConfig config);

        Entity Create(Vector3 position, EntityConfig config);
    }
}
