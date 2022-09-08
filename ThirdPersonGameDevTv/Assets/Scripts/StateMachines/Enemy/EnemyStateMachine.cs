using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        [field: SerializeField] public float PlayerChasingRange { get; private set; }
        
        public Animator Animator { get; private set; }
        public GameObject Player { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public ForceReceiver ForceReceiver { get; private set; }


        private void Awake()
        {
            Animator = GetComponent<Animator>();
            CharacterController = GetComponent<CharacterController>();
            ForceReceiver = GetComponent<ForceReceiver>();
        }

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            SwitchState(new EnemyIdleState(this));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
        }
    }
}
