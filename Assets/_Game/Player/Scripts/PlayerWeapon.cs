using LOK1game.Weapon;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerWeapon : MonoBehaviour, IPawnInput
    {
        public event Action<Gun> OnEquip;
        public event Action<Gun> OnDequip;

        public GunData CurrentGun { get; private set; }
        public Gun CurrentGunObject { get; private set; }

        public bool InAds
        {
            get
            {
                return CurrentGunObject.InADS;
            }
        }

        public List<GunData> WeaponInventory = new List<GunData>();

        [SerializeField] private Transform _gunHolder;

        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();

            Equip(WeaponInventory[0]);

            PlayerHudMobileInputManager.OnShootButtonClicked += Shoot;
        }

        public void OnInput(object sender)
        {
            if(Input.touchCount > 0) { return; }

            switch (CurrentGun.BurstMode)
            {
                case GunBurstMode.Semi:
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Shoot();
                    }
                    break;
                case GunBurstMode.Auto:
                    if (Input.GetButton("Fire1"))
                    {
                        Shoot();
                    }
                    break;
            }
        }

        public void Shoot()
        {
            CurrentGunObject.Shoot(_player);
        }

        public void Equip(GunData gunData)
        {
            if(CurrentGunObject != null)
            {
                OnDequip?.Invoke(CurrentGunObject);

                Destroy(CurrentGunObject);
            }

            CurrentGun = gunData;

            CurrentGunObject = (Gun)Instantiate(gunData.GunPrefab, _gunHolder);
            CurrentGunObject.GetComponent<Gun>().Equip(_player);

            OnEquip?.Invoke(CurrentGunObject);
        }
    }
}