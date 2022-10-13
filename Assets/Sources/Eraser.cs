using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class Eraser : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _raycastDistance = 10f;
    [SerializeField] private Texture2D[] _brushs;
    [SerializeField] private Level _tsunamiLevel;
    [SerializeField] private Color _clearColor;

    private SphereCollider _collider;
    private GameObject _previousCanvas;
    private Texture2D _texture;
    private float _scaleFactory;

    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        Vector3 startPoint = transform.position + (_collider.radius + _collider.radius) * Vector3.up;

        if (Physics.SphereCast(startPoint, _collider.radius, Vector3.down, out RaycastHit hit, _raycastDistance, _layer))
            Erase(hit.collider.gameObject);
    }

    private void Erase(GameObject canvas)
    {
        if (_texture == null || canvas != _previousCanvas)
        {
            _texture = canvas.GetComponent<Ground>().Texture;
            _scaleFactory = canvas.GetComponent<MeshFilter>().mesh.bounds.size.x / _texture.width;
            _previousCanvas = canvas;
        }

        Texture2D brush = GetBrush();
        int brushRadius = brush.width / 2;
        int x = (int)((transform.position.x - canvas.transform.position.x) / _scaleFactory) - brushRadius;
        int y = (int)((transform.position.z - canvas.transform.position.z) / _scaleFactory) - brushRadius;

        var brushRaw = brush.GetPixelData<Color32>(0);
        var canvasRaw = _texture.GetPixelData<Color32>(0);
        int brushIndex = 0;

        for (int i = 0; i < brush.height; i++)
        {
            for (int j = 0; j < brush.width; j++)
            {
                int xx = x + j;
                int canvasIndex = xx + (y + i) * _texture.height;

                if (brushRaw[brushIndex++].r == byte.MaxValue)
                    continue;

                if (xx < 0 || xx >= _texture.width)
                    continue;

                if (canvasIndex < 0 || canvasIndex >= canvasRaw.Length)
                    continue;

                canvasRaw[canvasIndex] = _clearColor;
            }
        }

        _texture.Apply();
    }

    private Texture2D GetBrush()
    {
        int index = Mathf.Clamp((int)_tsunamiLevel.Value - 1, 0, _brushs.Length - 1);
        return _brushs[index];
    }
}