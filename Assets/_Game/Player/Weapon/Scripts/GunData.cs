using UnityEngine;

namespace LOK1game.Weapon
{
    [CreateAssetMenu(fileName = "new GunData", menuName = "GunData")]
    public class GunData : ScriptableObject
    {
        public int Damage = 20;
        public float StartBulletForce = 80f;
        public int BulletsPerShoot = 6;
        public float FireRate = 0.2f;
        public float ShootDistance = 1000f;
        public GunBurstMode BurstMode;

        [Space]
        public float Bloom = 25;

        [Range(0.1f, 25f)]
        public float BloomXMultiplier = 1f;
        [Range(0.1f, 25f)]
        public float BloomYMultiplier = 1f;

        [Header("Recoil")]
        public float ShootFovChange = 0.4f;
        public Vector3 ShotCameraOffset;

        [Space]
        public Projectile ProjectilePrefab;
        public BaseGun GunPrefab;
        public GameObject ShellPrefab;
    }

    public enum GunBurstMode
    {
        Semi,
        Auto,
        Burst
    }
}