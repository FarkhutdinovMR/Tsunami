using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class Ground : MonoBehaviour
{    
    private Texture2D _texture;

    public Texture2D Texture => _texture;

    private void Start()
    {
        int _resolution = (int)(GetComponent<MeshFilter>().mesh.bounds.center.x * 256 / 10.24);
        _texture = new Texture2D(_resolution, _resolution, TextureFormat.RGBA32, false);
        _texture.filterMode = FilterMode.Trilinear;
        Material material = GetComponent<Renderer>().material;
        material.mainTexture = _texture;

        var data = _texture.GetRawTextureData<Color32>();
        int index = 0;

        for (int y = 0; y < _texture.height; y++)
            for (int x = 0; x < _texture.width; x++)
                data[index++] = material.color;

        _texture.Apply();
        material.color = Color.white;
    }
}