using UnityEngine;
using UnityEngine.Events;

namespace LOK1game.Weapon
{
    public class Gun : BaseGun
    {
        public UnityEvent OnShoot;

        private float _timeToNextShoot;

        public override void Shoot(Player.Player player)
        {
            if (Time.time > _timeToNextShoot)
            {
                _timeToNextShoot = Time.time + 1f / data.FireRate;

                for (int i = 0; i < data.BulletsPerShoot; i++)
                {
                    var camera = player.PlayerCamera.GetCameraTransform();
                    var projectile = Instantiate(data.ProjectilePrefab, muzzleTransform.position, Quaternion.identity);
                    var direction = camera.forward;

                    if (i != 0)
                    {
                        direction += GetBloom(camera);
                    }

                    var damage = new Damage(data.Damage, player);

                    projectile.Shoot(direction, data.StartBulletForce, damage);

                    OnShoot?.Invoke();
                }
            }
        }
    }
}