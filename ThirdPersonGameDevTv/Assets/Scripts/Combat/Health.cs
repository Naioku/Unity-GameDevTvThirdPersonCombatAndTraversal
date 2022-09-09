using System;
using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        public event Action OnTakeDamage;
        public event Action OnDie;
        
        public bool IsInvulnerable { get; set; }
        public bool IsDead => _currentHealth == 0;
        
        [SerializeField] private int maxHealth = 100;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead) return;
            if (IsInvulnerable) return;
            
            _currentHealth = Mathf.Max(_currentHealth - damage, 0);
            OnTakeDamage?.Invoke();
            print("Character health: " + _currentHealth);
            
            if (IsDead)
            {
                print("Character is dead xP");
                OnDie?.Invoke();
            }
        }
    }
}
