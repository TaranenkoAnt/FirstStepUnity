using UnityEngine;

public class FloatAnimation : MonoBehaviour
{
    public float floatHeight = 0.5f;
    public float floatSpeed = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Плавное движение вверх-вниз
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Медленное вращение
        transform.Rotate(0, 20f * Time.deltaTime, 0);
    }
}