using UnityEngine;
using UnityEngine.UI;

public class SpeedIndicator : MonoBehaviour
{
    private Text speedText;
    private Vector3 lastPosition;
    private float currentSpeed;

    void Start()
    {
        lastPosition = transform.position;

        // Автоматически находим Text на сцене
        speedText = GameObject.Find("SpeedText")?.GetComponent<Text>();

        if (speedText == null)
        {
            Debug.LogWarning("SpeedText не найден на сцене!");
        }
    }

    void Update()
    {
        // Вычисляем скорость
        currentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        // Обновляем UI если Text найден
        if (speedText != null)
        {
            speedText.text = $"Скорость: {currentSpeed:F1} м/с\n" +
                           $"Позиция: X:{transform.position.x:F0} Z:{transform.position.z:F0}";
        }
    }
}