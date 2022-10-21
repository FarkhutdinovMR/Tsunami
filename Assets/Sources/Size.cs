using System.Collections;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class Size : MonoBehaviour
{
    [SerializeField] private float _growSpeed = 1;
    [SerializeField] private Transform _model;

    private SphereCollider _collider;
    private float _targetValue = 1;
    private float _value = 1;
    private Coroutine _coroutine;

    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }

    public void Grow(uint level)
    {
        _targetValue = level;

        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(GrowSmoth());
    }

    private IEnumerator GrowSmoth()
    {
        while (_value < _targetValue)
        {
            _value += Time.deltaTime * _growSpeed;
            _model.localScale = Vector3.one * _value;
            _collider.radius = _value;
            yield return null;
        }

        _coroutine = null;
    }
}