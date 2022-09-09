using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerBlockState : PlayerBaseState
    {
        private static readonly int BlockHash = Animator.StringToHash("Block");
        private const float AnimationCrossFadeDuration = 0.1f;

        public PlayerBlockState(PlayerStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(BlockHash, AnimationCrossFadeDuration);
            StateMachine.Health.IsInvulnerable = true;
        }

        public override void Tick(float deltaTime)
        {
            if (StateMachine.Targeter.CurrentTarget == null )
            {
                StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
            }
            
            if (!StateMachine.InputReader.IsBlocking)
            {
                StateMachine.SwitchState(new PlayerTargetingState(StateMachine));
            }
        }

        public override void Exit()
        {
            StateMachine.Health.IsInvulnerable = false;
        }
    }
}
