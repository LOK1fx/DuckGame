using LOK1game.Weapon;
using UnityEngine;

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
        }
        private void Update()
        {
            //test
            OnInput(this);
        }

        public void OnInput(object sender)
        {
            PlayerCamera.OnInput(this);
        }
    }
}