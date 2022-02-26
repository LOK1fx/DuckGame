using UnityEngine;

namespace LOK1game.Weapon
{
    public abstract class BaseGun : MonoBehaviour, IUsable
    {
        [SerializeField] protected GunData data;

        public void Use(object sender)
        {
            if (sender is Player.Player)
            {
                Shoot((Player.Player)sender);
            }
        }

        public abstract void Shoot(Player.Player player);
    }
}