using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyChasingState : EnemyBaseState
    {
        private static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private const float AnimationCrossFadeDuration = 0.1f;
        private const float AnimatorDampTime = 0.05f;

        public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, AnimationCrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            FacePlayer();
            MoveToPlayer(deltaTime);
            
            StateMachine.Animator.SetFloat(Speed, 1f, AnimatorDampTime, deltaTime);

            if (!IsInChaseRange())
            {
                StateMachine.SwitchState(new EnemyIdleState(StateMachine));
            }
        }

        public override void Exit()
        {
            StateMachine.NavMeshAgent.ResetPath();
            StateMachine.NavMeshAgent.velocity = Vector3.zero;
        }

        private void MoveToPlayer(float deltaTime)
        {
            StateMachine.NavMeshAgent.destination = StateMachine.Player.transform.position;
            
            Move(StateMachine.NavMeshAgent.desiredVelocity.normalized * StateMachine.MovementSpeed, deltaTime);

            StateMachine.NavMeshAgent.velocity = StateMachine.CharacterController.velocity;
        }
    }
}
