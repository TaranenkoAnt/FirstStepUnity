using UnityEngine;

public class RandomBuildingColor : MonoBehaviour
{
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Случайный серый/бежевый цвет для зданий
            Color randomColor = new Color(
                Random.Range(0.6f, 0.9f),
                Random.Range(0.6f, 0.9f),
                Random.Range(0.6f, 0.9f)
            );
            renderer.material.color = randomColor;
        }
    }
}
