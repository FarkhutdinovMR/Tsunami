using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class LandscapeDerformer : MonoBehaviour
{
    [SerializeField] private float _depth = 0.5f;
    [SerializeField] private float _scale = 0.5f;
    [SerializeField] private Transform _raycastOrigin;
    [SerializeField] private float _raycastDistance = 10f;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private bool _recalculateNormals = false;

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
        float sqrRadius = _collider.radius * _collider.radius * _scale;

        for (int i = 0; i < vertisies.Length; i++)
        {
            float distance = (source.transform.position + vertisies[i] - transform.position).sqrMagnitude;
            if (distance > sqrRadius)
                continue;

            vertisies[i] -= new Vector3(0, _depth, 0);
        }

        _mesh.vertices = vertisies;

        if (_recalculateNormals)
            _mesh.RecalculateNormals();

        _previousObject = source;
    }
}