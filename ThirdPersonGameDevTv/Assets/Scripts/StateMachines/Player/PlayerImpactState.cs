using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerImpactState : PlayerBaseState
    {
        private static readonly int ImpactHash = Animator.StringToHash("Impact");
        private const float AnimationCrossFadeDuration = 0.1f;

        private float _duration;

        public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            _duration = StateMachine.ImpactDuration;
        }
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(ImpactHash, AnimationCrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            
            _duration -= deltaTime;
            if (_duration <= 0f)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit()
        {
            
        }
    }
}
