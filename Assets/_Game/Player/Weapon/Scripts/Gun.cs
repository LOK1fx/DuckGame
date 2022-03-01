using UnityEngine;
using UnityEngine.Events;

namespace LOK1game.Weapon
{
    public class Gun : BaseGun
    {
        public event UnityAction OnShoot;
        public event UnityAction OnEquip;

        [Space]
        [SerializeField] private float _adsSpeed = 8f;
        [SerializeField] private Vector3 _adsGunPositon;

        private Vector3 _defaultGunPosition;
        private Quaternion _defaultGunRotation;

        private float _timeToNextShoot;


        private void Start()
        {
            _defaultGunPosition = sightTransform.localPosition;
            _defaultGunRotation = sightTransform.localRotation;
        }

        private void Update()
        {
            var targetPos = _adsGunPositon;
            var targetRot = Quaternion.identity;
            var ads = Input.GetButton("Fire2");

            if(!ads)
            {
                targetPos = _defaultGunPosition;
                targetRot = _defaultGunRotation;
            }

            sightTransform.localPosition = Vector3.MoveTowards(sightTransform.localPosition, targetPos, Time.deltaTime * _adsSpeed);
            sightTransform.localRotation = Quaternion.Lerp(sightTransform.localRotation, targetRot, Time.deltaTime * _adsSpeed);

            InADS = ads;
        }

        public override void Equip(Player.Player player)
        {
            OnEquip?.Invoke();
        }

        public override void Shoot(Player.Player player)
        {
            if (Time.time > _timeToNextShoot)
            {
                _timeToNextShoot = Time.time + 1f / data.FireRate;

                for (int i = 0; i < data.BulletsPerShoot; i++)
                {
                    var camera = player.PlayerCamera.GetCameraTransform();
                    var projectile = Instantiate(data.ProjectilePrefab, camera.position, Quaternion.identity);
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