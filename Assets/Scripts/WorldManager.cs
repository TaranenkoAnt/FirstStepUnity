using UnityEngine;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour
{
    [System.Serializable]
    public class Region
    {
        public string regionName;
        public Vector3 centerPosition;
        public List<GameObject> buildings = new List<GameObject>();
        public GameObject regionMarker; // Визуальный маркер региона
    }

    [Header("World Generation Settings")]
    public List<Region> regions = new List<Region>();
    public int buildingsPerRegion = 5;
    public float regionSpacing = 150f;
    public float islandRadius = 25f; // Радиус островка

    [Header("Building Settings")]
    public GameObject buildingPrefab;
    public float minBuildingHeight = 15f;
    public float maxBuildingHeight = 35f;

    [Header("Debug Visualization")]
    public bool showRegionMarkers = true;
    public Material regionMarkerMaterial;

    void Start()
    {
        InitializeWorld();
        SpawnPlayerBoat();
    }

    void InitializeWorld()
    {
        CreateRegions();
        GenerateBuildings();
        CreateRegionMarkers();
    }

    void CreateRegions()
    {
        regions.Clear();

        // Определяем границы водной поверхности
        float waterSize = 500f; // Размер твоей водной поверхности
        float margin = 80f; // Отступ от краев воды

        // Список для отслеживания занятых позиций (чтобы регионы не пересекались)
        List<Vector3> usedPositions = new List<Vector3>();
        float minDistanceBetweenRegions = 80f; // Минимальное расстояние между центрами регионов

        for (int i = 0; i < 5; i++)
        {
            Region region = new Region();
            region.regionName = "Городской_Остров_" + (i + 1);

            // Пытаемся найти валидную позицию (без пересечений)
            Vector3 regionPos;
            int attempts = 0;
            bool positionValid = false;

            do
            {
                // Случайная позиция в пределах водной поверхности
                float x = Random.Range(-waterSize / 2 + margin, waterSize / 2 - margin);
                float z = Random.Range(-waterSize / 2 + margin, waterSize / 2 - margin);
                regionPos = new Vector3(x, 0f, z);

                // Проверяем, не слишком ли близко к другим регионам
                positionValid = true;
                foreach (Vector3 usedPos in usedPositions)
                {
                    if (Vector3.Distance(regionPos, usedPos) < minDistanceBetweenRegions)
                    {
                        positionValid = false;
                        break;
                    }
                }

                attempts++;
                if (attempts > 50) // Защита от бесконечного цикла
                {
                    Debug.LogWarning("Не удалось найти валидную позицию для региона " + i);
                    break;
                }
            } while (!positionValid);

            region.centerPosition = regionPos;
            regions.Add(region);
            usedPositions.Add(regionPos);

            Debug.Log($"Создан регион: {region.regionName} в позиции {region.centerPosition}");
        }
    }

    void GenerateBuildings()
    {
        if (buildingPrefab == null)
        {
            Debug.LogError("Building Prefab не назначен в WorldManager!");
            return;
        }

        foreach (Region region in regions)
        {
            for (int i = 0; i < buildingsPerRegion; i++)
            {
                // Случайная позиция в пределах островка
                Vector2 randomCircle = Random.insideUnitCircle * islandRadius;
                Vector3 buildingPosition = region.centerPosition +
                    new Vector3(randomCircle.x, 0f, randomCircle.y);

                // Создаем здание
                GameObject building = Instantiate(
                    buildingPrefab,
                    buildingPosition,
                    Quaternion.identity
                );

                // Случайная высота здания
                float randomHeight = Random.Range(minBuildingHeight, maxBuildingHeight);
                building.transform.localScale = new Vector3(
                    building.transform.localScale.x,
                    randomHeight,
                    building.transform.localScale.z
                );

                // Поднимаем здание так, чтобы оно "торчало" из воды
                building.transform.position = new Vector3(
                    building.transform.position.x,
                    randomHeight / 2f, // Центрируем по высоте
                    building.transform.position.z
                );

                building.name = $"Building_{region.regionName}_{i}";
                building.transform.SetParent(this.transform); // Организуем в иерархии

                region.buildings.Add(building);
            }
        }
    }

    void CreateRegionMarkers()
    {
        if (!showRegionMarkers) return;

        foreach (Region region in regions)
        {
            // Создаем визуальный маркер региона (плоский цилиндр)
            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            marker.name = $"Marker_{region.regionName}";
            marker.transform.position = region.centerPosition + Vector3.up * 0.1f;
            marker.transform.localScale = new Vector3(islandRadius * 2f, 0.1f, islandRadius * 2f);

            // Настраиваем материал
            Renderer renderer = marker.GetComponent<Renderer>();
            if (regionMarkerMaterial != null)
            {
                renderer.material = regionMarkerMaterial;
            }
            else
            {
                renderer.material.color = new Color(1f, 0f, 0f, 0.3f); // Полупрозрачный красный
            }

            region.regionMarker = marker;
        }
    }

    // Метод для отладки - показывает информацию о регионах
    void OnDrawGizmos()
    {
        if (regions == null) return;

        foreach (Region region in regions)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(region.centerPosition, islandRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(region.centerPosition, 5f);
        }
    }

    [Header("Player Settings")]
    public GameObject boatPrefab;
    public Transform boatSpawnPoint;

    void SpawnPlayerBoat()
    {
        if (boatPrefab != null)
        {
            // Спавним лодку по центру
            Vector3 spawnPos = new Vector3(0, 0.5f, 0);
            GameObject boat = Instantiate(boatPrefab, spawnPos, Quaternion.identity);
            boat.name = "PlayerBoat";

            Debug.Log("Лодка создана в позиции: " + spawnPos);

            // Назначаем лодку камере
            SetupCameraTarget(boat.transform);
        }
        else
        {
            Debug.LogError("Boat Prefab не назначен в WorldManager!");
        }
    }

    void SetupCameraTarget(Transform boatTransform)
    {
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.target = boatTransform;
            Debug.Log("Камера настроена на слежение за лодкой");
        }
        else
        {
            Debug.LogError("На основной камере не найден компонент CameraFollow!");
        }
    }
}