using UnityEngine;

namespace LOK1game.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 4f;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            Destroy(gameObject, _lifeTime);
        }

        public void Shoot(Vector3 direction, float force)
        {
            _rigidbody.AddForce(direction * force, ForceMode.VelocityChange);
        }
    }
}