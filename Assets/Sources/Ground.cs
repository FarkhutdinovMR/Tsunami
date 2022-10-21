using UnityEngine;

[RequireComponent (typeof(Renderer), typeof(MeshFilter))]
public class Ground : MonoBehaviour
{    
    private Texture2D _texture;

    public Texture2D Texture => _texture;

    private const float Resolution = 128;
    private const float Resolution2 = 10.24f;

    private void Start()
    {
        CreateTexture();
    }

    private void CreateTexture()
    {
        float meshHalfWidth = GetComponent<MeshFilter>().mesh.bounds.center.x;
        int _resolution = (int)(meshHalfWidth * Resolution / Resolution2);

        _texture = new Texture2D(_resolution, _resolution, TextureFormat.RGBA32, false);
        _texture.filterMode = FilterMode.Bilinear;

        Material material = GetComponent<Renderer>().material;
        material.mainTexture = _texture;

        FillColor(material.color);

        _texture.Apply();
        material.color = Color.white;
    }

    private void FillColor(Color color)
    {
        var data = _texture.GetRawTextureData<Color32>();
        int index = 0;

        for (int y = 0; y < _texture.height; y++)
            for (int x = 0; x < _texture.width; x++)
                data[index++] = color;
    }
}