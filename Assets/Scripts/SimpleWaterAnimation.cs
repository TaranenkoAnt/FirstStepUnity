using UnityEngine;

public class SimpleWaterAnimation : MonoBehaviour
{
    public float scrollSpeed = 0.05f;
    private Renderer waterRenderer;
    private float textureOffset = 0f;

    void Start()
    {
        waterRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Плавно меняем смещение текстуры
        textureOffset += Time.deltaTime * scrollSpeed;

        // Применяем смещение к материалу
        waterRenderer.material.mainTextureOffset = new Vector2(textureOffset, textureOffset * 0.5f);

        // Добавляем легкое изменение цвета для эффекта ряби
        float colorVariation = Mathf.Sin(Time.time * 0.5f) * 0.1f;
        Color waterColor = new Color(0.165f, 0.29f, 0.435f); // #2A4A6F в RGB
        waterRenderer.material.color = waterColor * (1f + colorVariation);
    }
}