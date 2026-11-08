using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Enemies.SpawnStrategies
{
    public class SwordsmanSpawnStrategy : IEnemySpawnStrategy
    {
        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;

        public SwordsmanSpawnStrategy(EntitiesFactory entitiesFactory, BrainsFactory brainsFactory)
        {
            _entitiesFactory = entitiesFactory;
            _brainsFactory = brainsFactory;
        }

        public bool CanHandle(EntityConfig config) => config is SwordsmanConfig;

        public Entity Create(Vector3 position, EntityConfig config, MonoEntity view = null)
        {
            SwordsmanConfig swordsmanConfig = (SwordsmanConfig)config;

            Entity entity = _entitiesFactory.CreateSwordsman(position, swordsmanConfig, view);
            _brainsFactory.CreateSwordsmanBrain(entity, swordsmanConfig.AttackRange, swordsmanConfig.SpotRangeDistance);
            return entity;
        }
    }
}
