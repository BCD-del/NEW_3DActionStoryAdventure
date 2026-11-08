using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Enemies.SpawnStrategies
{
    public class ArcherSpawnStrategy : IEnemySpawnStrategy
    {
        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;

        public ArcherSpawnStrategy(EntitiesFactory entitiesFactory, BrainsFactory brainsFactory)
        {
            _entitiesFactory = entitiesFactory;
            _brainsFactory = brainsFactory;
        }

        public bool CanHandle(EntityConfig config) => config is ArcherConfig;

        public Entity Create(Vector3 position, EntityConfig config, MonoEntity view = null)
        {
            ArcherConfig archerConfig = (ArcherConfig)config;

            Entity entity = _entitiesFactory.CreateArcher(position, archerConfig, view);
            _brainsFactory.CreateArcherBrain(entity, archerConfig.AttackRange, archerConfig.SpotRangeDistance);
            return entity;
        }
    }
}
