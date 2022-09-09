using System;
using UnityEngine;

namespace Combat
{
    /// <summary>
    /// GetComponentsInChildren<T>(true); and foreach loops with CompareTag() method are quite expensive, so be careful.
    /// As long as You use it not frequently it's ok...
    /// Here we use it only on the start of the game and when character dies.
    /// </summary>
    public class Ragdoll : MonoBehaviour
    {
        private Animator _animator;
        private CharacterController _characterController;

        private Collider[] _allColliders;
        private Rigidbody[] _allRigidbodies;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _allColliders = GetComponentsInChildren<Collider>(true);
            _allRigidbodies = GetComponentsInChildren<Rigidbody>(true);

            ToggleRagdoll(false);
        }

        public void ToggleRagdoll(bool state)
        {
            foreach (var collider in _allColliders)
            {
                if (collider.CompareTag("Ragdoll"))
                {
                    collider.enabled = state;
                }
            }
            
            foreach (var rigidbody in _allRigidbodies)
            {
                if (rigidbody.CompareTag("Ragdoll"))
                {
                    rigidbody.isKinematic = !state; // Use physics or not.
                    rigidbody.useGravity = state; // Use physics or not.
                }
            }

            _characterController.enabled = !state;
            _animator.enabled = !state;
        }
    }
}
