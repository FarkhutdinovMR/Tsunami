using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
public class VertexOffset : MonoBehaviour
{
    private void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertisies = mesh.vertices;

        for (int i = 0; i < vertisies.Length; i++)
            vertisies[i].y = i * 0.05f;

        mesh.vertices = vertisies;
    }
}