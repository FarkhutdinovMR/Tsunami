using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class Backlight : MonoBehaviour
{
    [SerializeField] private float _value = 0.5f;

    private Material[] _materials;
    private float _defaultValue;

    private const string _textureImpact = "_TextureImpact";

    private void Start()
    {
        _materials = GetComponent<Renderer>().materials;
        _defaultValue = _materials[0].GetFloat(_textureImpact);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TsunamiBacklightTrigger tsunami))
            foreach(Material material in _materials)
                material.SetFloat(_textureImpact, _value);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out TsunamiBacklightTrigger tsunami))
            foreach (Material material in _materials)
                material.SetFloat(_textureImpact, _defaultValue);
    }
}