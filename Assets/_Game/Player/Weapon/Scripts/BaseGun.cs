using UnityEngine;

namespace LOK1game.Weapon
{
    public abstract class BaseGun : MonoBehaviour, IUsable
    {
        public bool InADS { get; protected set; }

        [SerializeField] protected GunData data;

        [Space]
        [SerializeField] protected Transform muzzleTransform;
        [SerializeField] protected Transform sightTransform;

        public void Use(object sender)
        {
            if (sender is Player.Player)
            {
                Shoot((Player.Player)sender);
            }
        }

        public abstract void Shoot(Player.Player player);

        public abstract void Equip(Player.Player player);

        protected Vector3 GetBloom(Transform firePoint)
        {
            var bloom = firePoint.position + firePoint.forward * data.ShootDistance;

            bloom += CalculateBloom(firePoint.up) * data.BloomYMultiplier;
            bloom += CalculateBloom(firePoint.right) * data.BloomXMultiplier;
            bloom -= firePoint.position;

            return bloom.normalized;
        }

        private Vector3 CalculateBloom(Vector3 direction)
        {
            return Random.Range(-data.Bloom * 10f, data.Bloom * 10f) * direction;
        }

        public Transform GetSightTransform()
        {
            return sightTransform;
        }
    }
}