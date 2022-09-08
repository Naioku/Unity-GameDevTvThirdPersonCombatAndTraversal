using Combat;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        [field: SerializeField] public float PlayerChasingRange { get; private set; } // To expose it that way to inspector "set" must not be deleted.
        [field: SerializeField] public float MovementSpeed { get; private set; } = 5f; // To expose it that way to inspector "set" must not be deleted.
        [field: SerializeField] public float AttackRange { get; private set; } = 2f; // To expose it that way to inspector "set" must not be deleted.
        [field: SerializeField] public int AttackDamage { get; private set; } = 20; // To expose it that way to inspector "set" must not be deleted.
        [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; } // To expose it that way to inspector "set" must not be deleted.
        
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
