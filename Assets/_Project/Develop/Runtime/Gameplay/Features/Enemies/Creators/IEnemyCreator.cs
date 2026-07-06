using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Enemies.Creators
{
    // Каждый новый тип врага (лучник, мечник, маг и т.п.) реализует этот интерфейс.
    // EnemiesFactory больше не знает о конкретных типах - она просто перебирает
    // зарегистрированные IEnemyCreator и находит подходящий под тип конфига.
    public interface IEnemyCreator
    {
        bool CanCreate(EntityConfig config);

        Entity Create(Vector3 position, EntityConfig config);
    }
}
