using System;
using UnityEngine;

namespace LOK1game.World
{
    [RequireComponent(typeof(Collider))]
    public class Water : MonoBehaviour
    {
        public float WorldLevel = 5f;

        public event Action<GameObject> OnObjectEnter;
        public event Action<GameObject> OnObjectExit;

        private void OnTriggerEnter(Collider other)
        {
            if(Condition(other, out var floater))
            {
                floater.EnterWater(this);

                if(floater.Rigidbody.velocity.y <= -3f)
                {
                    if(TryGetComponent<BulletImpactEffect>(out var effect))
                    {
                        var damage = new Damage(0);

                        damage.HitPoint = floater.transform.position;
                        damage.HitNormal = Vector3.up;
                        
                        effect.TakeDamage(damage);
                    }
                }

                OnObjectEnter?.Invoke(floater.gameObject);

                Debug.Log($"Water enter: {floater.gameObject.name}");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (Condition(other, out var floater))
            {
                floater.LeaveWater(this);

                OnObjectExit?.Invoke(floater.gameObject);

                Debug.Log($"Water exit: {floater.gameObject.name}");
            }
        }

        private bool Condition(Collider collider, out WaterFloater floater)
        {
            if (collider.gameObject.TryGetComponent(out floater))
            {
                return true;
            }

            return false;
        }

        private void OnDrawGizmosSelected()
        {
            var size = new Vector3(transform.localScale.x, 0.0001f, transform.localScale.z);
            var position = new Vector3(transform.position.x, 0f, transform.position.z);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(position + (Vector3.one * WorldLevel), size);
        }
    }
}