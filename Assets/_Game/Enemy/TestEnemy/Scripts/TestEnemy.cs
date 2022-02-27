using UnityEngine;

namespace LOK1game.Test
{
    [RequireComponent(typeof(Health))]
    public class TestEnemy : Actor, IDamagable
    {
        [SerializeField] private float _resetHurtTime = 1f;
        [SerializeField] private Material _hurtedMaterial;
        [SerializeField] private MeshRenderer _mesh;

        private Material _defaultMaterial;
        private Health _health;

        private float _currentResetMatTimer;

        private void Start()
        {
            _health = GetComponent<Health>();
            _defaultMaterial = _mesh.sharedMaterial;
        }

        private void Update()
        {
            if(_currentResetMatTimer > 0)
            {
                _currentResetMatTimer -= Time.deltaTime;
            }
            else
            {
                _mesh.sharedMaterial = _defaultMaterial;
            }
        }

        public void TakeDamage(Damage damage)
        {
            _health.ReduceHealth(damage.Value);

            Debug.Log($"{gameObject.name}: Take the hit - {damage.Value}d");

            _mesh.sharedMaterial = _hurtedMaterial;
            _currentResetMatTimer = _resetHurtTime;
        }
    }
}