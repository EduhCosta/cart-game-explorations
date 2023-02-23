using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCartController : MonoBehaviour
{
    [SerializeField] public Rigidbody SphereCollider;

    [Header("Cart properties")]
    [SerializeField] public float Acceleration = 30f;
    [SerializeField] public float Steering = 30f;
    [SerializeField] public float Gravity = 10f;


    // Local variables
    private float _speed;
    private float _currentSpeed;
    private float _rotation;
    private float _currentRotation;
    private bool _isBreaking = false;

    // Loop variables
    private float _timeToAddForce;
    private float _newSpeed;

    private void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);

    }

    void Update()
    {
        // Follow sphere
        transform.position = SphereCollider.transform.position - new Vector3(0, 0.4f, 0);
        // Input Accelerate
        _speed = Input.GetAxis("Vertical") * Acceleration;
        // Input Steer
        _rotation = Input.GetAxis("Horizontal") * Steering;
        // Input Break
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isBreaking = true;
        }
        else
        {
            _isBreaking = false;
        }


        // Accelerate
        _timeToAddForce = _isBreaking ? 3f : 12f; // Time to break : Time to max acceletate
        _newSpeed = _isBreaking ? 0 : _speed;
        _currentSpeed = Mathf.SmoothStep(_currentSpeed, _newSpeed, Time.deltaTime * _timeToAddForce);
        // Steer
        _currentRotation = Mathf.Lerp(_currentRotation, _rotation, Time.deltaTime * 4f);
    }

  void FixedUpdate()
  {
    // Run
    SphereCollider.AddForce(transform.forward * _currentSpeed, ForceMode.Acceleration);

    //Gravity
    SphereCollider.AddForce(Vector3.down * Gravity, ForceMode.Acceleration);

    // Steering
    transform.eulerAngles = Vector3.Lerp(
        transform.eulerAngles,
        new Vector3(0, transform.eulerAngles.y + _currentRotation, 0),
        Time.deltaTime * 5f
    );
  }
}
