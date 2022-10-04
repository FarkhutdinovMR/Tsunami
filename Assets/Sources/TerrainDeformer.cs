using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TerrainDeformer : MonoBehaviour
{
    [SerializeField] private int _resolution = 10;
    [SerializeField] private float _depth = 1;
    [SerializeField] private float _scale = 1;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private bool _recalculateNormals = false;
    [SerializeField] private float _raycastDistance = 10f;
    [SerializeField] private Transform _raycastOrigin;

    private SphereCollider _collider;
    private GameObject _previousObject;
    private Mesh _mesh;

    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (Physics.SphereCast(_raycastOrigin.position, _collider.radius, Vector3.down, out RaycastHit hit, _raycastDistance, _layer))
            Deform(hit.collider.gameObject);
    }

    private void Deform(GameObject source)
    {
        if (_mesh == null || _previousObject != source)
            _mesh = source.GetComponent<MeshFilter>().mesh;

        Vector3[] vertisies = _mesh.vertices;

        int x = (int)(_collider.transform.position.x - source.transform.position.x);
        int z = (int)(_collider.transform.position.z - source.transform.position.z);

        int size = (int)(_collider.radius + _scale);

        for (int zz = -size + 1; zz < size; zz++)
        {
            for (int xx = -size + 1; xx < size; xx++)
            {
                int index = CalculateIndex(x + xx, z + zz);
                
                if (index < 0 || index >= vertisies.Length)
                    continue;

                if (vertisies[index].y > -_depth)
                    vertisies[index] += Vector3.down * _depth;
            }
        }

        _mesh.vertices = vertisies;

        if (_recalculateNormals)
            _mesh.RecalculateNormals();

        _previousObject = source;
    }

    public int CalculateIndex(int x, int z)
    {
        return x + z + z * _resolution;
    }
}