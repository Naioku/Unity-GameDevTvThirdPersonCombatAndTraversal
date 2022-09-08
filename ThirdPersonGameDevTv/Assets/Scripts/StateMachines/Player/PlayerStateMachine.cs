using Combat;
using Combat.Targeting;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; } = 6f; // To expose it that way to inspector "set" must not be deleted.
        [field: SerializeField] public float TargetingMovementSpeed { get; private set; } = 5f; // To expose it that way to inspector "set" must not be deleted.
        [field: SerializeField] public float RotationDamping { get; private set; } = 10f; // To expose it that way to inspector "set" must not be deleted.
        [field: SerializeField] public Targeter Targeter { get; private set; } // To expose it that way to inspector "set" must not be deleted.
        [field: SerializeField] public Attack[] Attacks { get; private set; } // To expose it that way to inspector "set" must not be deleted.
        [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; } // To expose it that way to inspector "set" must not be deleted.

        public InputReader InputReader { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public Animator Animator { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        public ForceReceiver ForceReceiver { get; private set; }
        
        private void Awake()
        {
            InputReader = GetComponent<InputReader>();
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            ForceReceiver = GetComponent<ForceReceiver>();
        }
        
        private void Start()
        {
            MainCameraTransform = Camera.main.transform;
            SwitchState(new PlayerFreeLookState(this));
        }
    }
}
