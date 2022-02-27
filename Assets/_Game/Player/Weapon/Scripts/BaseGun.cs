using UnityEngine;

namespace LOK1game.Weapon
{
    public abstract class BaseGun : MonoBehaviour, IUsable
    {
        [SerializeField] protected GunData data;

        [Space]
        [SerializeField] protected Transform muzzleTransform;

        public void Use(object sender)
        {
            if (sender is Player.Player)
            {
                Shoot((Player.Player)sender);
            }
        }

        public abstract void Shoot(Player.Player player);

        protected Vector3 GetBloom(Transform playerCamera)
        {
            var bloom = playerCamera.position + playerCamera.forward * data.ShootDistance;

            bloom += CalculateBloom(playerCamera.up) * data.BloomYMultiplier;
            bloom += CalculateBloom(playerCamera.right) * data.BloomXMultiplier;
            bloom -= playerCamera.position;

            return bloom.normalized;
        }

        private Vector3 CalculateBloom(Vector3 direction)
        {
            return Random.Range(-data.Bloom * 10f, data.Bloom * 10f) * direction;
        }
    }
}