using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.Enemies;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class SceneEntitiesSpawner
    {
        private readonly EnemiesFactory _enemiesFactory;

        public SceneEntitiesSpawner(EnemiesFactory enemiesFactory)
        {
            _enemiesFactory = enemiesFactory;
        }

        public void SpawnEnemies()
        {
            SceneEnemy[] sceneEnemies = Object.FindObjectsByType<SceneEnemy>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None);

            Debug.Log($"Врагов расставлено на сцене: {sceneEnemies.Length}");

            foreach (SceneEnemy sceneEnemy in sceneEnemies)
            {
                MonoEntity view = sceneEnemy.GetComponent<MonoEntity>();

                if (view == null)
                {
                    Debug.LogError($"{sceneEnemy.name}: нет компонента {nameof(MonoEntity)}, враг пропущен", sceneEnemy);
                    continue;
                }

                if (sceneEnemy.Config == null)
                {
                    Debug.LogError($"{sceneEnemy.name}: не выбран конфиг врага, враг пропущен", sceneEnemy);
                    continue;
                }

                if (view.gameObject.activeSelf == false)
                    view.gameObject.SetActive(true);

                _enemiesFactory.Create(view, sceneEnemy.Config);
            }
        }
    }
}
