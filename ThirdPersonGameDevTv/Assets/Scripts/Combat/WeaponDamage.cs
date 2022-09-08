using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class WeaponDamage : MonoBehaviour
    {
        [SerializeField] private Collider ownerCollider;
        
        private int _weaponDamage;
        private float _knockBack;
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

            if (other.TryGetComponent(out ForceReceiver forceReceiver))
            {
                forceReceiver.AddForce
                (
                    GetDirection(ownerCollider.transform.position, other.transform.position) * _knockBack
                );
            }
        }

        private Vector3 GetDirection(Vector3 from, Vector3 to)
        {
            return (to - from).normalized;
        }

        public void SetWeaponDamage(int damage, float knockBack)
        {
            _weaponDamage = damage;
            _knockBack = knockBack;
        }
    }
}
