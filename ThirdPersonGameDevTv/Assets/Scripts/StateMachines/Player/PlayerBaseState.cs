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

        protected void Move(Vector3 motion, float deltaTime)
        {
            StateMachine.CharacterController.Move((motion + StateMachine.ForceReceiver.ForceDisplacement) * deltaTime);
        }

        protected void FaceTarget()
        {

            Target target = StateMachine.Targeter.CurrentTarget;
            if (target == null) return;

            Vector3 pointingVector = target.transform.position - StateMachine.transform.position;
            pointingVector.y = 0f;

            StateMachine.transform.rotation = Quaternion.LookRotation(pointingVector);
            
            // StateMachine.transform.LookAt(target.transform.position);
        }
    }
}