using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Attack.Melee
{
    /// <summary>
    /// Рисует зону ближней атаки в Scene view (только в редакторе, только когда объект выделен).
    /// Параметры должны совпадать с MeleeAttackSystem, иначе будет видно не то, что бьёт в игре.
    /// </summary>
    public class MeleeAttackGizmo : MonoBehaviour
    {
        [SerializeField] private float radius = 1.8f;
        [SerializeField] private float forwardOffset = 1f;
        [SerializeField] private float heightOffset = 0.9f;

        private void OnDrawGizmosSelected()
        {
            if (transform == null)
                return;

            Vector3 hitCenter = transform.position
                + transform.forward * forwardOffset
                + Vector3.up * heightOffset;

            Gizmos.color = new Color(1f, 0.2f, 0.2f, 0.6f);
            Gizmos.DrawWireSphere(hitCenter, radius);

            // Линия направления удара
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + Vector3.up * heightOffset, hitCenter);
            Gizmos.DrawWireSphere(hitCenter, 0.15f);
        }
    }
}
