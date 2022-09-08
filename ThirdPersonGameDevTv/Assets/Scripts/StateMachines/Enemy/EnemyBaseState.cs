using UnityEngine;

namespace StateMachines.Enemy
{
    public abstract class EnemyBaseState : State
    {
        protected readonly EnemyStateMachine StateMachine;

        protected EnemyBaseState(EnemyStateMachine stateMachine)
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

        protected bool IsInChaseRange()
        {
            // float distanceFromPlayer = Vector3.Distance
            // (
            //     StateMachine.transform.position, 
            //     StateMachine.Player.transform.position
            // );
            //
            // return distanceFromPlayer <= StateMachine.PlayerChasingRange;

            // That can be better for performance, because computer doesn't have to root the squared magnitude.
            float distanceFromPlayerSquared = (StateMachine.transform.position - StateMachine.Player.transform.position).sqrMagnitude;

            return distanceFromPlayerSquared <= Mathf.Pow(StateMachine.PlayerChasingRange, 2);
        }

        protected void FacePlayer()
        {
            FaceTowards(StateMachine.gameObject, StateMachine.Player);
        }
    }
}
