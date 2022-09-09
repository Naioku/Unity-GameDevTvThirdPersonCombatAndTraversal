namespace StateMachines.Player
{
    public class PlayerDeathState : PlayerBaseState
    {
        public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.WeaponDamage.gameObject.SetActive(false);
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void Exit()
        {
        }
    }
}
