using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Weapons/Sword")]
    public class Sword : Weapon
    {
        public Sword()
        {
            base.Range = 0;
        }
    }
}