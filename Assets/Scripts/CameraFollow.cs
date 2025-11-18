using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 15, -10);
    public float smoothSpeed = 0.125f;

    void Start()
    {
        // ≈сли цель не назначена, попробуем найти лодку по тегу
        if (target == null)
        {
            GameObject boat = GameObject.FindGameObjectWithTag("Player");
            if (boat != null)
            {
                target = boat.transform;
                Debug.Log(" амера нашла лодку по тегу Player");
            }
        }

        // Ќемедленно устанавливаем позицию камеры
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = desiredPosition;
            transform.LookAt(target);
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target);
        }
    }
}