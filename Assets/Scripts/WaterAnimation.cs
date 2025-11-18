using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    public float waveSpeed = 0.5f;
    public float waveScale = 0.2f;

    private Mesh mesh;
    private Vector3[] baseVertices;
    private Vector3[] animatedVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        animatedVertices = new Vector3[baseVertices.Length];
    }

    void Update()
    {
        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];

            // Создаем волны с помощью синуса
            float wave = Mathf.Sin(Time.time * waveSpeed +
                                 vertex.x * 0.1f + vertex.z * 0.1f) * waveScale;

            animatedVertices[i] = new Vector3(vertex.x, wave, vertex.z);
        }

        mesh.vertices = animatedVertices;
        mesh.RecalculateNormals();
    }
}