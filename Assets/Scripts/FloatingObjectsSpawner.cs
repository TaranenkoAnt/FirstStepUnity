using UnityEngine;

public class FloatingObjectsSpawner : MonoBehaviour
{
    public GameObject markerPrefab;
    public int numberOfMarkers = 15;
    public float spawnAreaSize = 300f;

    void Start()
    {
        SpawnMarkers();
    }

    void SpawnMarkers()
    {
        if (markerPrefab == null)
        {
            Debug.LogError("Marker Prefab не назначен!");
            return;
        }

        for (int i = 0; i < numberOfMarkers; i++)
        {
            // Случайная позиция в области воды
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnAreaSize / 2, spawnAreaSize / 2),
                0.5f, // Над водой
                Random.Range(-spawnAreaSize / 2, spawnAreaSize / 2)
            );

            // Создаем маркер
            GameObject marker = Instantiate(markerPrefab, spawnPosition, Quaternion.identity);
            marker.name = "WaterMarker_" + i;

            // Добавляем анимацию плавания
            marker.AddComponent<FloatAnimation>();
        }

        Debug.Log("Создано плавающих маркеров: " + numberOfMarkers);
    }
}