using LOK1game.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerWeapon : MonoBehaviour
    {
        public GunData CurrentGun { get; private set; }
        public bool InAds
        {
            get
            {
                return _currentGunObject.InADS;
            }
        }

        public List<GunData> WeaponInventory = new List<GunData>();

        private BaseGun _currentGunObject;

        [SerializeField] private Transform _gunHolder;

        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();

            Equip(WeaponInventory[0]);
        }

        private void Update()
        {
            switch (CurrentGun.BurstMode)
            {
                case GunBurstMode.Semi:
                    if(Input.GetButtonDown("Fire1"))
                    {
                        _currentGunObject.Shoot(_player);
                    }
                    break;
                case GunBurstMode.Auto:
                    if (Input.GetButton("Fire1"))
                    {
                        _currentGunObject.Shoot(_player);
                    }
                    break;
            }
        }

        public void Equip(GunData gunData)
        {
            if(_currentGunObject != null)
            {
                Destroy(_currentGunObject);
            }

            CurrentGun = gunData;

            _currentGunObject = Instantiate(gunData.GunPrefab, _gunHolder);
            _currentGunObject.GetComponent<Gun>().Equip(_player);
        }
    }
}