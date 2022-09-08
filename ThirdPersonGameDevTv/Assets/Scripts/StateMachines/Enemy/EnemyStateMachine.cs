using UnityEngine;
using UnityEngine.AI;

namespace StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        [field: SerializeField] public float PlayerChasingRange { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;
        
        public Animator Animator { get; private set; }
        public GameObject Player { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public ForceReceiver ForceReceiver { get; private set; }
        public NavMeshAgent NavMeshAgent { get; private set; }


        private void Awake()
        {
            Animator = GetComponent<Animator>();
            CharacterController = GetComponent<CharacterController>();
            ForceReceiver = GetComponent<ForceReceiver>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");

            NavMeshAgent.updatePosition = false;
            NavMeshAgent.updateRotation = false;
            
            SwitchState(new EnemyIdleState(this));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
        }
    }
}
