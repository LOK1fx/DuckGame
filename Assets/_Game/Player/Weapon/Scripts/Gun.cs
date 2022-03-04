using UnityEngine;
using UnityEngine.Events;

namespace LOK1game.Weapon
{
    public class Gun : BaseGun
    {
        public event UnityAction OnShoot;
        public event UnityAction OnEquip;

        [Space]
        [SerializeField] private Vector3 _adsGunPositon;

        private Vector3 _defaultGunPosition;
        private Quaternion _defaultGunRotation;

        private float _timeToNextShoot;


        private void Start()
        {
            _defaultGunPosition = sightTransform.localPosition;
            _defaultGunRotation = sightTransform.localRotation;
        }

        public override void Equip(Player.Player player)
        {
            OnEquip?.Invoke();
        }

        public void SetAdsStatus(Player.Player player, bool ads)
        {
            var targetPos = Vector3.zero;
            var targetRot = Quaternion.identity;

            if(ads)
            {
                var cameraRot = player.PlayerCamera.GetCameraTransform().localRotation.eulerAngles;

                targetPos = _adsGunPositon;
                targetRot = Quaternion.Euler(cameraRot.x, cameraRot.y, 0f);


                RotateSight(Quaternion.identity);
            } 
            else
            {
                targetPos = _defaultGunPosition;

                RotateSight(_defaultGunRotation);
            }
            
            sightTransform.localPosition = Vector3.MoveTowards(sightTransform.localPosition, targetPos, Time.deltaTime * data.AdsSpeed);
            transform.localRotation = targetRot;

            InADS = ads;
        }

        private void RotateSight(Quaternion target)
        {
            sightTransform.localRotation = Quaternion.RotateTowards(sightTransform.localRotation, target, Time.deltaTime * data.AdsSpeed);
        }

        public override void Shoot(Player.Player player)
        {
            if (Time.time > _timeToNextShoot)
            {
                _timeToNextShoot = Time.time + 1f / data.FireRate;

                for (int i = 0; i < data.BulletsPerShoot; i++)
                {
                    var camera = player.PlayerCamera.GetCameraTransform();
                    var projectilePos = muzzleTransform.position;
                    var direction = muzzleTransform.forward;

                    if (!data.ShootsFromMuzzle)
                    {
                        projectilePos = camera.position;
                        direction = camera.forward;
                    }

                    var projectile = Instantiate(data.ProjectilePrefab, projectilePos, Quaternion.identity);

                    if (i != 0)
                    {
                        direction += GetBloom(muzzleTransform);
                    }
                    else
                    {
                        direction.z = data.ShootDistance;
                    }

                    var damage = new Damage(data.Damage, player);

                    projectile.Shoot(direction, data.StartBulletForce, damage);

                    OnShoot?.Invoke();
                }
            }
        }
    }
}