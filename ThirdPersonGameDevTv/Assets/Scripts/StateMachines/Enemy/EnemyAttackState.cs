using Combat;
using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private const float AnimationCrossFadeDuration = 0.1f;

        public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.WeaponDamage.SetWeaponDamage(StateMachine.AttackDamage, StateMachine.AttackKnockBack);
            StateMachine.Animator.CrossFadeInFixedTime(AttackHash, AnimationCrossFadeDuration);
            
            FacePlayer();
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            
            if (GetNormalizedAnimationTime(StateMachine.Animator) >= 1f)
            {
                StateMachine.SwitchState(new EnemyChasingState(StateMachine));
            }
        }

        public override void Exit()
        {
        }
    }
}
