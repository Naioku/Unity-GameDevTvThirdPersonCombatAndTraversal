using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private static readonly int TargetingLocomotion = Animator.StringToHash("TargetingLocomotion");
        private static readonly int TargetingMovementForwardSpeedHash = Animator.StringToHash("TargetingMovementForwardSpeed");
        private static readonly int TargetingMovementRightSpeedHash = Animator.StringToHash("TargetingMovementRightSpeed");
        private const float AnimationCrossFadeDuration = 0.1f;
        private const float AnimatorDampTime = 0.05f;

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(TargetingLocomotion, AnimationCrossFadeDuration);
            StateMachine.InputReader.CancelEvent += OnCancelState;
            StateMachine.InputReader.AttackEvent += OnAttack;
        }

        public override void Tick(float deltaTime)
        {
            if (StateMachine.Targeter.CurrentTarget == null)
            {
                StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
            }

            if (StateMachine.InputReader.IsBlocking)
            {
                StateMachine.SwitchState(new PlayerBlockState(StateMachine));
            }

            Move(CalculateMovement() * StateMachine.TargetingMovementSpeed, deltaTime);
            UpdateAnimator(deltaTime);
            FaceTarget();
        }

        public override void Exit()
        {
            StateMachine.InputReader.CancelEvent -= OnCancelState;
            StateMachine.InputReader.AttackEvent -= OnAttack;
        }

        private void OnCancelState()
        {
            StateMachine.Targeter.Cancel();
            StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
        }

        private Vector3 CalculateMovement()
        {
            var movement = new Vector3();

            movement += StateMachine.transform.right * StateMachine.InputReader.MovementValue.x;
            movement += StateMachine.transform.forward * StateMachine.InputReader.MovementValue.y;

            return movement;
        }

        private void UpdateAnimator(float deltaTime)
        {
            float movementRightValue = StateMachine.InputReader.MovementValue.x;
            float movementForwardValue = StateMachine.InputReader.MovementValue.y;

            if (movementForwardValue == 0f)
            {
                StateMachine.Animator.SetFloat(TargetingMovementForwardSpeedHash, 0, AnimatorDampTime, deltaTime);
            }
            else
            {
                float value = movementForwardValue > 0f ? 1f : -1f;
                StateMachine.Animator.SetFloat(TargetingMovementForwardSpeedHash, value, AnimatorDampTime, deltaTime);
            }
            
            if (movementRightValue == 0f)
            {
                StateMachine.Animator.SetFloat(TargetingMovementRightSpeedHash, 0, AnimatorDampTime, deltaTime);
            }
            else
            {
                float value = movementRightValue > 0f ? 1f : -1f;
                StateMachine.Animator.SetFloat(TargetingMovementRightSpeedHash, value, AnimatorDampTime, deltaTime);
            }
        }
        
        private void OnAttack()
        {
            StateMachine.SwitchState(new PlayerAttackState(StateMachine, 0));
        }
    }
}
