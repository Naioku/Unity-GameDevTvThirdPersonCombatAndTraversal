using Combat;
using UnityEngine;

namespace StateMachines.Player
{
    public class AttackingState : PlayerBaseState
    {
        private float _previousFrameTime;
        private bool _isComboBroken;

        private readonly Attack _attack;
        private bool _hasForceAlreadyApplied;

        public AttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
        {
            _attack = StateMachine.Attacks[attackIndex];
        }

        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
            StateMachine.InputReader.AttackEvent += TryComboAttack;
            StateMachine.WeaponDamage.SetWeaponDamage(_attack.Damage);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            FaceTarget();

            if (GetNormalizedAnimationTime() >= 1f)
            {
                if (StateMachine.Targeter.CurrentTarget == null)
                {
                    StateMachine.SwitchState(new FreeLookState(StateMachine));
                }
                else
                {
                    StateMachine.SwitchState(new TargetingState(StateMachine));
                }
            }
            
            if (GetNormalizedAnimationTime() >= _attack.ForceTime)
            {
                TryForceApplication();
            }
        }

        public override void Exit()
        {
            StateMachine.InputReader.AttackEvent -= TryComboAttack;
        }
        
        private void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }

        private void TryComboAttack()
        {
            if (!HasCombo()) return;
            if (!ReadyForNextAttack(GetNormalizedAnimationTime()))
            {
                _isComboBroken = true;
                return;
            }
            if (_isComboBroken) return;

            StateMachine.SwitchState
            (
                new AttackingState
                (
                    StateMachine,
                    _attack.NextAttackIndex
                )
            );
        }

        /// <summary>
        /// Normalized time - The integer part is the number of time a state has been looped.
        /// The fractional part is the % (0-1) of progress in the current loop.
        ///
        /// It gets the normalized time of currently played animation tagged with "Attack".
        /// </summary>
        /// <returns></returns>
        private float GetNormalizedAnimationTime()
        {
            AnimatorStateInfo currentInfo = StateMachine.Animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = StateMachine.Animator.GetNextAnimatorStateInfo(0);

            if (StateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
            {
                return nextInfo.normalizedTime;
            }

            if (!StateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
            {
                return currentInfo.normalizedTime;
            }

            return 0f;
        }

        private bool ReadyForNextAttack(float normalizedTime)
        {
            return normalizedTime > _attack.NextComboAttackTime;
        }

        private bool HasCombo()
        {
            return _attack.NextAttackIndex >= 0;
        }

        private void TryForceApplication()
        {
            if (_hasForceAlreadyApplied) return;
            
            StateMachine.ForceReceiver.AddForce(StateMachine.transform.forward * _attack.ForceValue);
            _hasForceAlreadyApplied = true;
            
        }
    }
}
