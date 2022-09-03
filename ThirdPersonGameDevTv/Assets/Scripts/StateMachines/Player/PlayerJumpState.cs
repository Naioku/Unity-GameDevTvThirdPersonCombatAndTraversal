using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerJumpState : PlayerBaseState
    {
        public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            Debug.Log("Jump Enter.");
        }
        
        public override void Tick(float deltaTime)
        {
            Debug.Log("Jump Tick.");
        }
        
        public override void Exit()
        {
            Debug.Log("Jump Exit.");
        }
    }
}
