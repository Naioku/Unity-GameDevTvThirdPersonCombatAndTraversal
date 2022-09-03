namespace StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        private InputReader _inputReader;
        
        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
        }
        
        private void Start()
        {
            SwitchState(new PlayerTestState(this));
        }

        private void SwitchToJumpState() => SwitchState(new PlayerJumpState(this));
        private void SwitchToDodgeState() => SwitchState(new PlayerDodgeState(this));

        private void OnEnable()
        {
            _inputReader.JumpEvent += SwitchToJumpState;
            _inputReader.DodgeEvent += SwitchToDodgeState;
        }

        private void OnDisable()
        {
            _inputReader.JumpEvent -= SwitchToJumpState;
            _inputReader.DodgeEvent -= SwitchToDodgeState;
        }
    }
}
