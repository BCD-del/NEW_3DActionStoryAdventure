using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Enemies
{
    [RequireComponent(typeof(MonoEntity))]
    public class SceneEnemy : MonoBehaviour
    {
        [SerializeField] private EntityConfig _config;

        public EntityConfig Config => _config;
    }
}
