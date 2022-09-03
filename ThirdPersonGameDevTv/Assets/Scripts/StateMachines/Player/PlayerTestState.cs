using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        private float _timer = 5f;
        
        public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            Debug.Log("Enter");
        }

        public override void Tick(float deltaTime)
        {
            _timer -= deltaTime;
            
            Debug.Log($"Time left to next state change: {_timer}");

            if (_timer <= 0f)
            {
                StateMachine.SwitchState(new PlayerTestState(StateMachine));
            }
        }

        public override void Exit()
        {
            Debug.Log("Exit");
        }
    }
}
