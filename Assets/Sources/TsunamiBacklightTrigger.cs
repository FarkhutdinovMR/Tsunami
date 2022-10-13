using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class TsunamiBacklightTrigger : MonoBehaviour
{
    [SerializeField] private Level _tsunamiLevel;

    private SphereCollider _collider;
    private float _radius;

    private void OnEnable()
    {
        _collider = GetComponent<SphereCollider>();
        _radius = _collider.radius;
        _tsunamiLevel.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _tsunamiLevel.LevelChanged -= OnLevelChanged;
    }

    private void Update()
    {
        transform.position = _tsunamiLevel.transform.position + _tsunamiLevel.transform.forward * _collider.radius;
    }

    private void OnLevelChanged(uint level)
    {
        _collider.radius = level * _radius;
    }
}