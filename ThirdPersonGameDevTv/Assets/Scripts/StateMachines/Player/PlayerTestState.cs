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
            var movementVector = CalculateMovementVectorFromCameraPosition();

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
        
        private Vector3 CalculateMovementVectorFromCameraPosition()
        {
            // Coordinates of the vector pointing the forward direction from the camera in the world space coordinate system.
            Vector3 forwardVector = StateMachine.MainCameraTransform.forward;
            
            // Coordinates of the vector pointing the right direction from the camera in the world space coordinate system.
            Vector3 rightVector = StateMachine.MainCameraTransform.right;

            forwardVector.y = 0f;
            rightVector.y = 0f;
            
            forwardVector.Normalize();
            rightVector.Normalize();

            return forwardVector * StateMachine.InputReader.MovementValue.y +
                   rightVector * StateMachine.InputReader.MovementValue.x;
        }
    }
}
