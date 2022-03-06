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
            //t
            if (Application.isMobilePlatform)
            {
                Application.targetFrameRate = 120;
            }        

            PlayerCamera = GetComponent<PlayerCamera>();
            PlayerWeapon = GetComponent<PlayerWeapon>();
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
    }
}