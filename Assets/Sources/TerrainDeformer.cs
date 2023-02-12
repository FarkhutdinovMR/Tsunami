using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TerrainDeformer : MonoBehaviour
{
    [SerializeField] private float _scale = 1;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private bool _recalculateNormals = false;
    [SerializeField] private float _raycastDistance = 10f;

    private CharacterController _characterController;
    private GameObject _previousObject;
    private Mesh _mesh;
    private int _vertexPerWidth = 41;
    private int _vertexPerUnit = 2;
    private Vector3[] _vertisies;
    private Vector3 _sourcePosition;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 startPoint = transform.position + (_characterController.radius + _characterController.radius) * Vector3.up;

        if (Physics.SphereCast(startPoint, _characterController.radius, Vector3.down, out RaycastHit hit, _raycastDistance, _layer))
            Deform(hit.collider.gameObject);
    }

    private void Deform(GameObject source)
    {
        if (_mesh == null || _previousObject != source)
        {
            _mesh = source.GetComponent<MeshFilter>().mesh;
            _vertisies = _mesh.vertices;
            _vertexPerWidth = (int)Mathf.Sqrt(_mesh.vertexCount);
            _vertexPerUnit = (int)(_vertexPerWidth / _mesh.bounds.size.x);
            _sourcePosition = source.transform.position;
        }

        int xCenter = (int)(transform.position.x - source.transform.position.x);
        int zCenter = (int)(transform.position.z - source.transform.position.z);
        int indexCenter = CalculateIndex(xCenter, zCenter);
        Vector3 position = transform.position + _characterController.center;

        int size = (int)(_characterController.radius * _vertexPerUnit + _scale);
        for (int z = -size; z < size; z++)
        {
            for (int x = -size; x < size; x++)
            {
                int index = indexCenter + x + 1 + _vertexPerWidth * (z + 1);

                if (index < 0 || index >= _vertisies.Length)
                    continue;

                float distance = Vector3.Distance(position, _sourcePosition + _vertisies[index]);
                float height = (_characterController.radius) - distance;
                if (height <= 0f)
                    continue;

                _vertisies[index] += height * Vector3.down;
            }
        }

        _mesh.vertices = _vertisies;

        if (_recalculateNormals)
            _mesh.RecalculateNormals();

        _previousObject = source;
    }

    public int CalculateIndex(int x, int z)
    {
        return x * _vertexPerUnit + z * _vertexPerUnit * _vertexPerWidth;
    }
}