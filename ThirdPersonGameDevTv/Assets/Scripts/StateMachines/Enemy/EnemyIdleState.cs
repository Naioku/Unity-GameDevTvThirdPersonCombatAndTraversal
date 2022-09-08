using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        private static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private const float AnimationCrossFadeDuration = 0.1f;
        private const float AnimatorDampTime = 0.05f;

        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, AnimationCrossFadeDuration);
        }
        
        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            StateMachine.Animator.SetFloat(Speed, 0f, AnimatorDampTime, deltaTime);
            
            if (IsInChaseRange()) Debug.Log("Chasing player.");
        }

        public override void Exit()
        {
        }
    }
}
