using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 3f;

        public InputReader InputReader { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public Animator Animator { get; private set; }
        
        private void Awake()
        {
            InputReader = GetComponent<InputReader>();
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
        }
        
        private void Start()
        {
            SwitchState(new PlayerTestState(this));
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
