using UnityEngine;

public class GravityReceiver : MonoBehaviour
{
    private CharacterController _characterController;
    
    private float _verticalVelocity;

    public Vector3 GravityVector => Vector3.up * _verticalVelocity;

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
    }
}
