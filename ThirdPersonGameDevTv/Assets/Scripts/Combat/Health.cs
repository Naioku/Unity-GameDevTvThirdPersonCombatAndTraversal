using System;
using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        public event Action OnTakeDamage;
        
        [SerializeField] private int maxHealth = 100;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (_currentHealth == 0)
            {
                print("Character is dead xP");
                return;
            }
            
            _currentHealth = Mathf.Max(_currentHealth - damage, 0);
            OnTakeDamage?.Invoke();
            print("Character health: " + _currentHealth);
        }
    }
}
