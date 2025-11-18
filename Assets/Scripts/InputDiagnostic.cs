using UnityEngine;

public class InputDiagnostic : MonoBehaviour
{
    void Update()
    {
        // Проверка старых Input осей
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0) Debug.Log($"Horizontal: {horizontal}");
        if (vertical != 0) Debug.Log($"Vertical: {vertical}");

        // Проверка прямых клавиш
        if (Input.GetKey(KeyCode.W)) Debug.Log("W pressed");
        if (Input.GetKey(KeyCode.S)) Debug.Log("S pressed");
        if (Input.GetKey(KeyCode.A)) Debug.Log("A pressed");
        if (Input.GetKey(KeyCode.D)) Debug.Log("D pressed");

        // Проверка любых клавиш
        if (Input.anyKeyDown) Debug.Log("Какая-то клавиша нажата: " + Input.inputString);
    }
}