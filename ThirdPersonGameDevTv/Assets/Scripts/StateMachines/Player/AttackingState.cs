using Combat;
using UnityEngine;

namespace StateMachines.Player
{
    public class AttackingState : PlayerBaseState
    {
        private float _previousFrameTime;
        private bool _isComboBroken;

        private readonly Attack _attack;

        public AttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
        {
            _attack = StateMachine.Attacks[attackIndex];
        }

        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
            StateMachine.InputReader.AttackEvent += TryComboAttack;
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            FaceTarget();

            if (GetNormalizedAnimationTime() >= 1f)
            {
                StateMachine.SwitchState(new FreeLookState(StateMachine));
            }
        }

        public override void Exit()
        {
            StateMachine.InputReader.AttackEvent -= TryComboAttack;
            Debug.Log("Exiting the: AttackingState");
        }
        
        private void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
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
        
        private void TryComboAttack()
        {
            if (GetNormalizedAnimationTime() < _previousFrameTime)
            {
                _previousFrameTime = GetNormalizedAnimationTime();
                return;
            }
            _previousFrameTime = GetNormalizedAnimationTime();

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

        private bool ReadyForNextAttack(float normalizedTime)
        {
            return normalizedTime > _attack.ComboAttackTime;
        }

        private bool HasCombo()
        {
            return _attack.NextAttackIndex >= 0;
        }
    }
}
