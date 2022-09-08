using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private float impactSmoothingTime;
    
    private CharacterController _characterController;
    private NavMeshAgent _navMeshAgent;
    private float _verticalVelocity;
    private Vector3 _impact;
    private Vector3 _dampingVelocity;

    public Vector3 ForceDisplacement => _impact + Vector3.up * _verticalVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_verticalVelocity < 0f && _characterController.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, impactSmoothingTime);

        /*
         * It is important to check _impact.sqrMagnitude < 0.2f * 0.2f (no _impact == Vector3.zero), because
         * if it would be like before, after the impact character would be running at the spot.
         * It happens so, because in ImpactState (Player and Enemy) we have set specific duration.
         * But here we enabling NavMeshAgent (so the enemy moving feature too) only when the _impact gets down to the
         * Vector3.zero and it could take "a little" longer than duration of the 1 second (like it is/was set by default).
         *
         * And we checking sqrMagnitude rather than magnitude, because the multiplication is faster than division (in new CPUs(?)).
         */
        if (_navMeshAgent != null && _impact.sqrMagnitude < 0.2f * 0.2f)
        {
            _impact = Vector3.zero;
            _navMeshAgent.enabled = true;
        }
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;

        if (_navMeshAgent != null)
        {
            _navMeshAgent.enabled = false;
        }
    }
}
