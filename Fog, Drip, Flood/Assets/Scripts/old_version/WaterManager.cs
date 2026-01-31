using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class WaterManager : MonoBehaviour
{

    private MeshFilter MeshFilter;

    private void Awake()
    {
        MeshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        Vector3[] vertices = MeshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + vertices[i].x);
        }

        MeshFilter.mesh.vertices = vertices;
        MeshFilter.mesh.RecalculateNormals();
    }

}
