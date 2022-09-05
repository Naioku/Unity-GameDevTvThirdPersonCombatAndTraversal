using UnityEngine;

namespace StateMachines.Player
{
    public class AttackingState : PlayerBaseState
    {
        private float _timer = 2f;
        public AttackingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            Debug.Log("Entering the: AttackingState");
        }

        public override void Tick(float deltaTime)
        {
            _timer -= deltaTime;
            if (_timer <= 0f) StateMachine.SwitchState(new FreeLookState(StateMachine));
        }

        public override void Exit()
        {
            Debug.Log("Exiting the: AttackingState");

        }
    }
}
