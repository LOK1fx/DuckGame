using UnityEngine;

namespace LOK1game.World
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class TrainingPlate : Actor, IDamagable
    {
        [SerializeField] private float _startTorqueForce = 15f;

        [Space]
        [SerializeField] private Rigidbody _destroyFx;
        [SerializeField] private float _lifeTime = 6f;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            Destroy(gameObject, _lifeTime);
        }

        public void TakeDamage(Damage damage)
        {
            DestroyPlate();
        }

        public void Shoot(Vector3 direction, float force)
        {
            _rigidbody.AddForce(direction * force, ForceMode.VelocityChange);
            _rigidbody.AddTorque(transform.up * _startTorqueForce, ForceMode.VelocityChange);
        }

        private void DestroyPlate()
        {
            var fx = Instantiate(_destroyFx, transform.position, transform.rotation);

            fx.velocity = _rigidbody.velocity;

            Destroy(fx.gameObject, 2f);
            Destroy(gameObject);
        }
    }
}