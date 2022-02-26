using UnityEngine;
using UnityEngine.Events;

namespace LOK1game.Weapon
{
    public class Gun : BaseGun
    {
        public UnityEvent OnShoot;

        public override void Shoot(Player.Player player)
        {
            for (int i = 0; i < data.BulletsPerShoot; i++)
            {
                var camera = player.PlayerCamera.GetCameraTransform();
                var projectile = Instantiate(data.ProjectilePrefab, camera.transform.position, Quaternion.identity);
                var direction = camera.forward + GetBloom(camera);
                
                projectile.Shoot(direction, data.StartBulletForce);

                OnShoot?.Invoke();
            }
        }

        private Vector3 GetBloom(Transform playerCamera)
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