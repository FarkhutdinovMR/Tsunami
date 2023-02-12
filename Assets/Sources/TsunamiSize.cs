using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TsunamiSize : MonoBehaviour
{
    [SerializeField] private float _growSpeed = 1;
    [SerializeField] private Transform _model;

    private CharacterController _characterController;
    private float _targetValue = 1;
    private float _value = 1;
    private Coroutine _coroutine;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
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
            _characterController.radius = _value;
            _characterController.center = new Vector3(_characterController.center.x, _characterController.radius, _characterController.center.z);
            yield return null;
        }

        _coroutine = null;
    }
}
