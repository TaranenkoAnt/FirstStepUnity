using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float rotationSpeed = 60f;

    private Vector2 inputDirection;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        // �������� ����� ������
        if (inputDirection.y != 0)
        {
            Vector3 force = transform.forward * inputDirection.y * moveSpeed;
            rb.AddForce(force, ForceMode.Acceleration);
        }

        // ������� ����� ������
        if (inputDirection.x != 0)
        {
            float torque = inputDirection.x * rotationSpeed;
            rb.AddTorque(0, torque, 0, ForceMode.Acceleration);
        }

        // ������������ ������������ ��������
        if (rb.linearVelocity.magnitude > moveSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }
    }
}