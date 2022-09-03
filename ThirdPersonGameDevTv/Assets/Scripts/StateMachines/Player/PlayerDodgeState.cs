using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerDodgeState : PlayerBaseState
    {
        public PlayerDodgeState(PlayerStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            Debug.Log("Dodge Enter.");
        }
        
        public override void Tick(float deltaTime)
        {
            Debug.Log("Dodge Tick.");
        }
        
        public override void Exit()
        {
            Debug.Log("Dodge Exit.");
        }
    }
}
