using UnityEngine;

public class CarDriving : MonoBehaviour
{
    [SerializeField] private Rigidbody _sphereRigidbody;
    [SerializeField] private Rigidbody _carRigidbody;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _reverseSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _airDrag;
    [SerializeField] private float _groundDrag;
    [SerializeField] private LayerMask _groundLayer;
    
    private float _moveInput;
    private float _turnInput;
    
    private bool _isCarGrounded;

    private void Start()
    {
        DetachedSphereRigidbodyFromCar();
        DetachedCarRigidbodyFromCar();
        SetSphereDrag();
    }

    private void Update()
    {
        GetInputAxis();
        CheckForwardInput();
        CarFollowSphere();
        SetCarRotation();
        GroundCheck();
        CheckRigidbodyDrag();
    }

    private void FixedUpdate()
    {
        if (_isCarGrounded)
        {
            MoveCarForward();
        }
        else
        {
            AddGravity();
        }

        RotateCar();
    }

    private void CarFollowSphere()
    {
        transform.position = _sphereRigidbody.transform.position;
    }

    private void DetachedSphereRigidbodyFromCar()
    {
        _sphereRigidbody.transform.parent = null;
    }
    
    private void DetachedCarRigidbodyFromCar()
    {
        _carRigidbody.transform.parent = null;
    }

    private void CheckForwardInput()
    {
        _moveInput *= _moveInput > 0 ? _forwardSpeed : _reverseSpeed;
    }

    private void MoveCarForward()
    {
        _sphereRigidbody.AddForce(transform.forward * _moveInput, ForceMode.Acceleration);
    }

    private void AddGravity()
    {
        _sphereRigidbody.AddForce(transform.up  * -30f);
    }

    private void GetInputAxis()
    {
        _moveInput = Input.GetAxisRaw("Vertical");
        _turnInput = Input.GetAxisRaw("Horizontal");
    }

    private void SetCarRotation()
    {
        float newRotation = _turnInput * _turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0, Space.World);
    }

    private void GroundCheck()
    {
        _isCarGrounded = Physics.Raycast(transform.position, -transform.up, out var hit,1f, _groundLayer);
        
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
    }

    private void CheckRigidbodyDrag()
    {
        _sphereRigidbody.drag = _isCarGrounded ? _groundDrag : _airDrag;
    }

    private void RotateCar()
    {
        _carRigidbody.MoveRotation(transform.rotation);
    }

    private void SetSphereDrag()
    {
        _groundDrag = _sphereRigidbody.drag;
    }
}