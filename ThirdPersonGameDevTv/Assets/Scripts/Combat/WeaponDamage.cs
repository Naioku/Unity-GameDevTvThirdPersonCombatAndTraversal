using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class WeaponDamage : MonoBehaviour
    {
        [SerializeField] private Collider ownerCollider;
        
        private int _weaponDamage;
        private readonly List<Collider> _alreadyCollidedWith = new();

        private void OnEnable()
        {
            _alreadyCollidedWith.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == ownerCollider) return;

            if (_alreadyCollidedWith.Contains(other)) return;
            
            _alreadyCollidedWith.Add(other);
            
            if (other.TryGetComponent(out Health health))
            {
                health.TakeDamage(_weaponDamage);
            }
        }

        public void SetWeaponDamage(int damage)
        {
            _weaponDamage = damage;
        }
    }
}
