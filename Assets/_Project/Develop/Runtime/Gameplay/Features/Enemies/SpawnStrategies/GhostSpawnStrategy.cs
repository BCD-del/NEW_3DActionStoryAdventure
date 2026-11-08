using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Enemies.SpawnStrategies
{
    public class GhostSpawnStrategy : IEnemySpawnStrategy
    {
        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;

        public GhostSpawnStrategy(EntitiesFactory entitiesFactory, BrainsFactory brainsFactory)
        {
            _entitiesFactory = entitiesFactory;
            _brainsFactory = brainsFactory;
        }

        public bool CanHandle(EntityConfig config) => config is GhostConfig;

        public Entity Create(Vector3 position, EntityConfig config, MonoEntity view = null)
        {
            Entity entity = _entitiesFactory.CreateGhost(position, (GhostConfig)config, view);
            _brainsFactory.CreateGhostBrain(entity);
            return entity;
        }
    }
}
