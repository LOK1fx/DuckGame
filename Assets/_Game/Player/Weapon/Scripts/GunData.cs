using UnityEngine;

namespace LOK1game.Weapon
{
    [CreateAssetMenu(fileName = "new GunData", menuName = "GunData")]
    public class GunData : ScriptableObject
    {
        public int Damage;
        public float ShootDistance = 1000f;
        public int BulletsPerShoot = 6;
        public float FireRate = 0.2f;
        public float StartBulletForce = 80f;

        [Space]
        public float Bloom = 25;

        [Range(0.1f, 25f)]
        public float BloomXMultiplier = 1f;
        [Range(0.1f, 25f)]
        public float BloomYMultiplier = 1f;

        [Space]
        public Projectile ProjectilePrefab;
        public GameObject GunPrefab;
        public GameObject ShellPrefab;
    }
}