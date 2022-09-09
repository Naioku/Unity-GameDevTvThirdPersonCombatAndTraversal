using Combat;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerAttackState : PlayerBaseState
    {
        private float _previousFrameTime;
        private bool _isComboBroken;

        private readonly Attack _attack;
        private bool _hasForceAlreadyApplied;

        public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
        {
            _attack = StateMachine.Attacks[attackIndex];
        }

        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
            StateMachine.InputReader.AttackEvent += TryComboAttack;
            StateMachine.WeaponDamage.SetWeaponDamage(_attack.Damage, _attack.KnockBack);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            FaceTarget();

            if (GetNormalizedAnimationTime(StateMachine.Animator) >= 1f)
            {
               ReturnToLocomotion();
            }
            
            if (GetNormalizedAnimationTime(StateMachine.Animator) >= _attack.ForceTime)
            {
                TryForceApplication();
            }
        }

        public override void Exit()
        {
            StateMachine.InputReader.AttackEvent -= TryComboAttack;
        }

        private void TryComboAttack()
        {
            if (!HasCombo()) return;
            if (!ReadyForNextAttack(GetNormalizedAnimationTime(StateMachine.Animator)))
            {
                _isComboBroken = true;
                return;
            }
            if (_isComboBroken) return;

            StateMachine.SwitchState
            (
                new PlayerAttackState
                (
                    StateMachine,
                    _attack.NextAttackIndex
                )
            );
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
