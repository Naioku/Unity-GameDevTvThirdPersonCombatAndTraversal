using UnityEngine;

namespace StateMachines.Player
{
    public class TargetingState : PlayerBaseState
    {
        private static readonly int TargetingLocomotion = Animator.StringToHash("TargetingLocomotion");

        public TargetingState(PlayerStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.Animator.Play(TargetingLocomotion);
            StateMachine.InputReader.CancelEvent += OnCancel;
        }

        public override void Tick(float deltaTime)
        {
            if (StateMachine.Targeter.CurrentTarget == null)
            {
                StateMachine.SwitchState(new FreeLookState(StateMachine));
            }
        }

        public override void Exit()
        {
            StateMachine.InputReader.CancelEvent -= OnCancel;
        }

        private void OnCancel()
        {
            StateMachine.Targeter.Cancel();
            StateMachine.SwitchState(new FreeLookState(StateMachine));
        }
    }
}
