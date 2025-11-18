using UnityEngine;

public class TestBoatMove : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        // Простейшее движение вперед
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Debug.Log("Лодка движется! Позиция: " + transform.position);
    }
}
