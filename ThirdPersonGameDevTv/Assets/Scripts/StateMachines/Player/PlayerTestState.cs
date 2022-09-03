using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            Debug.Log("Enter");
        }

        public override void Tick(float deltaTime)
        {
            Vector2 movementInputValue = StateMachine.InputReader.MovementValue;
            var movementVector = new Vector3()
            {
                x = movementInputValue.x,
                y = 0,
                z = movementInputValue.y
            };
            
            StateMachine.CharacterController.Move(movementVector * StateMachine.MovementSpeed * deltaTime);
            
            if (StateMachine.InputReader.MovementValue == Vector2.zero) return;
            StateMachine.transform.rotation = Quaternion.LookRotation(movementVector);
        }

        public override void Exit()
        {
            Debug.Log("Exit");
        }
    }
}
