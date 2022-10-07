using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _raycastDistance = 10f;
    [SerializeField] private Transform _raycastOrigin;
    [SerializeField] private Texture2D[] _brushs;
    [SerializeField] private Color _aplhaColor;
    [SerializeField] private Tsunami _tsunami;

    private SphereCollider _collider;
    private GameObject _previousObject;
    private Texture2D _canvas;
    private float _scaleFactory;

    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (Physics.SphereCast(_raycastOrigin.position, _collider.radius, Vector3.down, out RaycastHit hit, _raycastDistance, _layer))
            Paint(hit.collider.gameObject);
    }

    private void Paint(GameObject source)
    {
        if (_canvas == null || source != _previousObject)
        {
            _canvas = source.GetComponent<Ground>().Texture;
            _scaleFactory = source.GetComponent<MeshFilter>().mesh.bounds.size.x / _canvas.width;
            _previousObject = source;
        }

        Texture2D brush = GetBrush();
        int brushRadius = brush.width / 2;
        int x = (int)((transform.position.x - source.transform.position.x) / _scaleFactory) - brushRadius;
        int y = (int)((transform.position.z - source.transform.position.z) / _scaleFactory) - brushRadius;

        //for (int i = 0; i < brush.height; i++)
        //{
        //    for (int j = 0; j < brush.width; j++)
        //    {
        //        int xx = x + j;
        //        int yy = y + i;

        //        if (xx < 0 || xx > _canvas.width || yy < 0 || yy > _canvas.height)
        //            continue;

        //        if (brush.GetPixel(j, i).r == 1)
        //            continue;

        //        _canvas.SetPixel(xx, yy, _aplhaColor);
        //    }
        //}

        var brushRaw = brush.GetPixelData<Color32>(0);
        var canvasRaw = _canvas.GetPixelData<Color32>(0);
        int brushIndex = 0;

        for (int i = 0; i < brush.height; i++)
        {
            for (int j = 0; j < brush.width; j++)
            {
                int canvasIndex = (x + j) + (y + i) * _canvas.height;

                if (brushRaw[brushIndex++].r == byte.MaxValue)
                    continue;

                if (canvasIndex < 0 || canvasIndex >= canvasRaw.Length)
                    continue;

                canvasRaw[canvasIndex] = _aplhaColor;
            }
        }

        _canvas.Apply();
    }

    private Texture2D GetBrush()
    {
        int index = Mathf.Clamp((int)_tsunami.Level - 1, 0, _brushs.Length - 1);
        return _brushs[index];
    }
}