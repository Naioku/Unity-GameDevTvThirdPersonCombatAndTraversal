using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyDeathState : EnemyBaseState
    {
        public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.WeaponDamage.gameObject.SetActive(false);
            Object.Destroy(StateMachine.Target);
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void Exit()
        {
        }
    }
}
