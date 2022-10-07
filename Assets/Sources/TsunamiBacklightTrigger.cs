using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class TsunamiBacklightTrigger : MonoBehaviour
{
    [SerializeField] private Tsunami _tsunami;

    private SphereCollider _collider;
    private float _radius;

    private void OnEnable()
    {
        _collider = GetComponent<SphereCollider>();
        _radius = _collider.radius;
        _tsunami.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _tsunami.LevelChanged -= OnLevelChanged;
    }

    private void Update()
    {
        transform.position = _tsunami.transform.position + _tsunami.transform.forward * _collider.radius;
    }

    private void OnLevelChanged(uint level)
    {
        _collider.radius = level * _radius;
    }
}