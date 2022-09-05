using Combat;
using UnityEngine;

namespace StateMachines.Player
{
    public class AttackingState : PlayerBaseState
    {
        private float _timer = 2f;
        private readonly Attack _attack;

        public AttackingState(PlayerStateMachine stateMachine, int attackId) : base(stateMachine)
        {
            _attack = StateMachine.Attacks[attackId];
        }

        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, 0.1f);
        }

        public override void Tick(float deltaTime)
        {
            Debug.Log(_attack.AnimationName);
            
            _timer -= deltaTime;
            if (_timer <= 0f) StateMachine.SwitchState(new FreeLookState(StateMachine));
        }

        public override void Exit()
        {
            Debug.Log("Exiting the: AttackingState");

        }
    }
}
