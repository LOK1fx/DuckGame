using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LOK1game
{
    [RequireComponent(typeof(Health))]
    public class Duck : Actor, IDamagable
    {
        public UnityEvent OnDie;

        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        public void TakeDamage(Damage damage)
        {
            _health.ReduceHealth(damage.Value);

            if(_health.Hp <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            OnDie?.Invoke();

            gameObject.SetActive(false);
        }
    }
}