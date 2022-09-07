using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class WeaponDamage : MonoBehaviour
    {
        [SerializeField] private Collider ownerCollider;
        [SerializeField] private int weaponDamage = 5;

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
                health.TakeDamage(weaponDamage);
            }
        }
    }
}
