using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    [SerializeField] private GameObject[] _wheelsToRotate;
    [SerializeField] private float _rotationSpeed;
    
    private float _verticalInput;
    private void Update()
    {
        GetInputAxis();
        RotateWheels();
    }

    private void GetInputAxis()
    {
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void RotateWheels()
    {
        foreach (var wheel in _wheelsToRotate)
        {
            wheel.transform.Rotate(Time.deltaTime * _verticalInput * _rotationSpeed,0,0, Space.Self);
        }
    }
}
