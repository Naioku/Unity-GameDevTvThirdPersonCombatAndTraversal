using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        private static readonly int MovementSpeedHash = Animator.StringToHash("FreeLookMovementSpeed");
        private static readonly int FreeLookLocomotionHash = Animator.StringToHash("FreeLookLocomotion");
        private const float AnimationCrossFadeDuration = 0.1f;
        private const float AnimatorDampTime = 0.05f;
        
        public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(FreeLookLocomotionHash, AnimationCrossFadeDuration);
            StateMachine.InputReader.TargetEvent += OnTarget;
            StateMachine.InputReader.AttackEvent += OnAttack;
        }

        public override void Tick(float deltaTime)
        {
            var movementVector = CalculateMovementVectorFromCameraPosition();

            Move(movementVector * StateMachine.FreeLookMovementSpeed, deltaTime);

            if (StateMachine.InputReader.MovementValue == Vector2.zero)
            {
                StateMachine.Animator.SetFloat(MovementSpeedHash, 0f, AnimatorDampTime, deltaTime);
                return;
            }
            
            FaceMovementDirection(movementVector);
            StateMachine.Animator.SetFloat(MovementSpeedHash, 1f, AnimatorDampTime, deltaTime);
        }

        public override void Exit()
        {
            StateMachine.InputReader.TargetEvent -= OnTarget;
            StateMachine.InputReader.AttackEvent -= OnAttack;
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
        
        private void OnTarget()
        {
            if (!StateMachine.Targeter.SelectTarget()) return;
            
            StateMachine.SwitchState(new PlayerTargetingState(StateMachine));
        }
        
        private void OnAttack()
        {
            StateMachine.SwitchState(new PlayerAttackingState(StateMachine, 0));
        }
    }
}
