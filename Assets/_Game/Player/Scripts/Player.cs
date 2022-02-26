using LOK1game.Weapon;
using UnityEngine;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerCamera))]
    public class Player : Actor, IPawnInput
    {
        public PlayerCamera PlayerCamera { get; private set; }

        [SerializeField] private GunData _startGun;
        [SerializeField] private Transform _gunHolder;

        private Gun _currentGun;

        private void Awake()
        {
            PlayerCamera = GetComponent<PlayerCamera>();
        }

        private void Start()
        {
            var gun = Instantiate(_startGun.GunPrefab, _gunHolder);

            _currentGun = gun.GetComponent<Gun>();
        }

        private void Update()
        {
            //test
            OnInput(this);
        }

        public void OnInput(object sender)
        {
            PlayerCamera.OnInput(this);

            if(Input.GetButtonDown("Fire1"))
            {
                if(_currentGun != null)
                {
                    _currentGun.Shoot(this);
                }
            }
        }
    }
}