using Assets._Project.Develop.Runtime.Gameplay.Features.Enemies.SpawnStrategies;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.LootFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Enemies
{
    public class EnemiesFactory
    {
        private readonly DIContainer _container;

        private readonly EntitiesFactory _entitiesFactory;
        private readonly IReadOnlyList<IEnemySpawnStrategy> _spawnStrategies;
        private readonly BrainsFactory _brainsFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly DropLootService _dropLootService;

        public EnemiesFactory(DIContainer container, IReadOnlyList<IEnemySpawnStrategy> spawnStrategies)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _dropLootService = _container.Resolve<DropLootService>();
            _spawnStrategies = spawnStrategies;
        }

        public Entity Create(Vector3 position, EntityConfig config)
        {
            Debug.Log($"Spawning enemy with config: {(config == null ? "NULL" : config.name)}");
            IEnemySpawnStrategy strategy = FindStrategyFor(config);

            Entity entity = strategy.Create(position, config);

            AddDropLootBehaviourTo(entity);

            entity.AddTeam(new ReactiveVariable<Teams>(Teams.Enemies));

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        /*  private IEnemySpawnStrategy FindStrategyFor(EntityConfig config)
          {
              foreach (IEnemySpawnStrategy strategy in _spawnStrategies)
              {
                  if (strategy.CanHandle(config))
                      return strategy;
              }

              throw new ArgumentException($"No {nameof(IEnemySpawnStrategy)} registered for {config.GetType()}");
          }*/

        private IEnemySpawnStrategy FindStrategyFor(EntityConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config), "EntityConfig cannot be null.");

            foreach (IEnemySpawnStrategy strategy in _spawnStrategies)
            {
                if (strategy.CanHandle(config))
                    return strategy;
            }

            throw new ArgumentException($"No {nameof(IEnemySpawnStrategy)} registered for {config.GetType()}");
        }

        private void AddDropLootBehaviourTo(Entity entity)
        {
            ICompositeCondition dropLootCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.LootIsDropped.Value == false));

            entity
                .AddLootIsDropped()
                .AddCanDropLoot(dropLootCondition);

            entity.MustSelfRelease.Add(new FuncCondition(() => entity.LootIsDropped.Value));

            entity.AddSystem(new DropLootSystem(_dropLootService));
        }
    }
}
