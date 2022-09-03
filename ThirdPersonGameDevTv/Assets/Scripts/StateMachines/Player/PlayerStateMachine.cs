namespace StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    { 
        public InputReader InputReader { get; private set; }
        
        private void Awake()
        {
            InputReader = GetComponent<InputReader>();
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
