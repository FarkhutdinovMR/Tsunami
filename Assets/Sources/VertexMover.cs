using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
public class VertexMover : MonoBehaviour
{
    [SerializeField] private float _yOffset;
    [SerializeField] private int _index;

    private Mesh _mesh;
    Vector3[] _vertisies;

    private void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        _vertisies = _mesh.vertices;
        for (int i = 0; i < _vertisies.Length; i++)
            _vertisies[i] += i * _yOffset * Vector3.up;

        _mesh.vertices = _vertisies;
        Debug.Log(_vertisies.Length);
    }

    private void Update()
    {
        //_vertisies[_index] = Vector3.up * _yOffset;
        //_mesh.vertices = _vertisies;
    }
}