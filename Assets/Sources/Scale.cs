using System.Collections;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] private Tsunami _tsunami;
    [SerializeField] private Transform _model;
    [SerializeField] private float _speed = 1;
    [SerializeField] private SphereCollider _collider;

    private float _targetScale = 1;
    private float _currentScale = 1;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _tsunami.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _tsunami.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(uint level)
    {
        _targetScale = level;

        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(GrowUp());
    }

    private IEnumerator GrowUp()
    {
        while (_currentScale < _targetScale)
        {
            _currentScale += Time.deltaTime * _speed;
            _model.localScale = Vector3.one * _currentScale;
            _collider.radius = _currentScale * 0.5f;
            yield return null;
        }

        _coroutine = null;
    }
}