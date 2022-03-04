using UnityEngine;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerCamera), typeof(PlayerWeapon))]
    public class Player : Actor, IPawnInput
    {
        public PlayerCamera PlayerCamera { get; private set; }
        public PlayerWeapon PlayerWeapon { get; private set; }

        private void Awake()
        {
            Application.targetFrameRate = 120;

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

            camera.AddCameraOffset(PlayerWeapon.CurrentGun.ShotCameraOffset);
        }
    }
}