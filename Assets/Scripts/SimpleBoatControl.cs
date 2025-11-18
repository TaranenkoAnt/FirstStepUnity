using UnityEngine;

public class SimpleBoatControl : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float rotationSpeed = 60f;

    void Update()
    {
        // Простейшее управление через GetKey
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            Debug.Log("Движение вперед");
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            Debug.Log("Движение назад");
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            Debug.Log("Поворот влево");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            Debug.Log("Поворот вправо");
        }
    }
}