using UnityEngine;
using LOK1game;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerCamera))]
    public class Player : Actor, IPawnInput
    {
        public PlayerCamera PlayerCamera { get; private set; }
        public PlayerWeapon PlayerWeapon { get; private set; }

        private void Awake()
        {
            PlayerCamera = GetComponent<PlayerCamera>();
            PlayerWeapon = GetComponent<PlayerWeapon>();

            PlayerWeapon.OnEquip += OnGunEquip;
            PlayerWeapon.OnDequip += OnGunDequip;
        }

        private void Update()
        {
            OnInput(this);

            if(PlayerWeapon.InAds)
            {
                PlayerCamera.SmoothSetFov(45);
            }
            else
            {
                PlayerCamera.SmoothSetFov(PlayerCamera.GetDefaultFov());
            }
        }

        public void OnInput(object sender)
        {
            PlayerCamera.OnInput(this);
            PlayerWeapon.OnInput(this);
        }

        private void OnGunDequip(Weapon.Gun gun)
        {
            gun.OnShoot -= OnGunShoot;
        }

        private void OnGunEquip(Weapon.Gun gun)
        {
            gun.OnShoot += OnGunShoot;
        }

        private void OnGunShoot()
        {
            var camera = PlayerCamera;

            //camera.SetFov(camera.GetCurrentFov() + PlayerWeapon.CurrentGun.ShootFovChange);
            camera.AddCameraOffset(PlayerWeapon.CurrentGun.ShotCameraOffset);
        }
    }
}