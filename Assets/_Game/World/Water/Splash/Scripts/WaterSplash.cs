using UnityEngine;

namespace LOK1game.World
{
    public class WaterSplash : MonoBehaviour
    {
        [SerializeField] private float _explosionForce = 7f;
        [SerializeField] private float _radius = 3f;
        [SerializeField] private LayerMask _layer;

        private void Start()
        {
            var colliders = Physics.OverlapSphere(transform.position,
                _radius, _layer, QueryTriggerInteraction.Ignore);

            foreach (var collider in colliders)
            {
                if(collider.TryGetComponent<WaterFloater>(out var floater))
                {
                    floater.Rigidbody.AddExplosionForce(_explosionForce, transform.position, _radius);
                }
            }
        }
    }
}