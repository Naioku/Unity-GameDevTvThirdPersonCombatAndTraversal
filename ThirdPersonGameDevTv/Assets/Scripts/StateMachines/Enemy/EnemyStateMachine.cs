using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        public Animator Animator { get; private set; }

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            SwitchState(new EnemyIdleState(this));
        }
    }
}
