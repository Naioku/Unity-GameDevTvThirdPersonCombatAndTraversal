using UnityEngine;

namespace StateMachines.Player
{
    public class FreeLookState : PlayerBaseState
    {
        private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
        private const float AnimatorDampTime = 0.05f;
        
        public FreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) {}

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
                StateMachine.Animator.SetFloat(MovementSpeed, 0f, AnimatorDampTime, deltaTime);
                return;
            }
            
            FaceMovementDirection(movementVector);
            StateMachine.Animator.SetFloat(MovementSpeed, 1f, AnimatorDampTime, deltaTime);
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
        
        private void FaceMovementDirection(Vector3 movementVector)
        {
            StateMachine.transform.rotation = Quaternion.Lerp(
                StateMachine.transform.rotation,
                Quaternion.LookRotation(movementVector),
                Time.deltaTime * StateMachine.RotationDamping);
        }
    }
}
