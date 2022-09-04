using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 6f; // To expose it that way to inspector "set" must not be deleted.
        [field: SerializeField] public float RotationDamping { get; private set; } = 10f; // To expose it that way to inspector "set" must not be deleted.

        public InputReader InputReader { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public Animator Animator { get; private set; }
        public Transform MainCameraTransform { get; private set; }

        
        private void Awake()
        {
            InputReader = GetComponent<InputReader>();
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
        }
        
        private void Start()
        {
            MainCameraTransform = Camera.main.transform;
            SwitchState(new FreeLookState(this));
        }

        private void SwitchToJumpState() => SwitchState(new PlayerJumpState(this));
        private void SwitchToDodgeState() => SwitchState(new PlayerDodgeState(this));

        private void OnEnable()
        {
            InputReader.JumpEvent += SwitchToJumpState;
            InputReader.DodgeEvent += SwitchToDodgeState;
        }

        private void OnDisable()
        {
            InputReader.JumpEvent -= SwitchToJumpState;
            InputReader.DodgeEvent -= SwitchToDodgeState;
        }
    }
}
