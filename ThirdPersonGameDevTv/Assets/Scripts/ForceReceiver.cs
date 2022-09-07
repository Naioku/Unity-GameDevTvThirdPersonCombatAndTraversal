using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private float impactSmoothingTime;
    
    private CharacterController _characterController;
    private float _verticalVelocity;
    private Vector3 _impact;
    private Vector3 _dampingVelocity;

    public Vector3 ForceDisplacement => _impact + Vector3.up * _verticalVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
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
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
    }
}
