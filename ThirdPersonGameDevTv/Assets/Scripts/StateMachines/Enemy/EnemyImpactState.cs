using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyImpactState : EnemyBaseState
    {
        private static readonly int ImpactHash = Animator.StringToHash("Impact");
        private const float AnimationCrossFadeDuration = 0.1f;

        private float _duration;

        public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
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
                StateMachine.SwitchState(new EnemyIdleState(StateMachine));
            }
        }

        public override void Exit()
        {
            
        }
    }
}
