using UnityEngine;

public class BuildingVariants : MonoBehaviour
{
    [Header("Building Variations")]
    public Material[] buildingMaterials;
    public Color[] buildingColors;

    void Start()
    {
        ApplyRandomVariant();
    }

    void ApplyRandomVariant()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && buildingMaterials.Length > 0)
        {
            // Случайный материал или цвет
            if (buildingMaterials.Length > 0)
            {
                renderer.material = buildingMaterials[Random.Range(0, buildingMaterials.Length)];
            }
            else if (buildingColors.Length > 0)
            {
                renderer.material.color = buildingColors[Random.Range(0, buildingColors.Length)];
            }
        }
    }
}
