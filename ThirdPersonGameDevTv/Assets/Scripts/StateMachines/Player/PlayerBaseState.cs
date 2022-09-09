using Combat.Targeting;
using UnityEngine;

namespace StateMachines.Player
{
    public abstract class PlayerBaseState : State
    {
        protected readonly PlayerStateMachine StateMachine;
        
        protected PlayerBaseState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }
        
        protected void Move(Vector3 motion, float deltaTime)
        {
            StateMachine.CharacterController.Move((motion + StateMachine.ForceReceiver.ForceDisplacement) * deltaTime);
        }

        protected void FaceTarget()
        {
            Target target = StateMachine.Targeter.CurrentTarget;
            if (target == null) return;

            FaceTowards(StateMachine.gameObject, target.gameObject);
        }
        
        protected void ReturnToLocomotion()
        {
            if (StateMachine.Targeter.CurrentTarget == null)
            {
                StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
            }
            else
            {
                StateMachine.SwitchState(new PlayerTargetingState(StateMachine));
            }
        }
    }
}