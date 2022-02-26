using System;
using UnityEngine;

namespace LOK1game.World
{
    [RequireComponent(typeof(Collider))]
    public class Water : MonoBehaviour
    {
        public event Action<GameObject> OnObjectEnter;
        public event Action<GameObject> OnObjectExit;

        private void OnTriggerEnter(Collider other)
        {
            if(Condition(other, out var rigidbody))
            {
                OnObjectEnter?.Invoke(rigidbody.gameObject);

                Debug.Log($"Water enter: {rigidbody.gameObject.name}");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (Condition(other, out var rigidbody))
            {
                OnObjectExit?.Invoke(rigidbody.gameObject);

                Debug.Log($"Water exit: {rigidbody.gameObject.name}");
            }
        }

        private bool Condition(Collider collider, out Rigidbody rigidbody)
        {
            if (collider.gameObject.TryGetComponent<Rigidbody>(out rigidbody))
            {
                return true;
            }

            return false;
        }
    }
}