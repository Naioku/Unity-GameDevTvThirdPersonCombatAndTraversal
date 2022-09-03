using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
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

            if (StateMachine.InputReader.MovementValue == Vector2.zero)
            {
                StateMachine.Animator.SetFloat(MovementSpeed, 0f, 0.05f, deltaTime);
                return;
            }
            
            StateMachine.transform.rotation = Quaternion.LookRotation(movementVector);
            StateMachine.Animator.SetFloat(MovementSpeed, 1f, 0.05f, deltaTime);
        }

        public override void Exit()
        {
            Debug.Log("Exit");
        }
    }
}
