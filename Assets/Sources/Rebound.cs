using System;
using System.Collections;
using UnityEngine;

public class Rebound : MonoBehaviour
{
    [SerializeField] private AnimationCurve _down;
    [SerializeField] private AnimationCurve _back;
    [SerializeField] private float _speed = 1;

    private float _runningTime;
    private Vector3 _previousPosition;
    private Coroutine _coroutine;
    private Action _action;

    public void Play(Transform obstacle, Action action)
    {
        if (obstacle == null)
            throw new ArgumentNullException(nameof(obstacle));

        if (_coroutine != null)
            return;

        _action = action;
        _coroutine = StartCoroutine(PlayAnimation(obstacle));
    }

    private IEnumerator PlayAnimation(Transform obstacle)
    {
        _runningTime = 0;
        _previousPosition = transform.position;
        Vector3 direction = obstacle.position - transform.position;
        direction.y = 0;
        direction = direction.normalized;

        while (_runningTime <= 1)
        {
            _runningTime += _speed * Time.deltaTime;
            var offset = direction * _back.Evaluate(_runningTime);

            transform.position = _previousPosition + offset;
            transform.localScale = Vector3.one - Vector3.down * _down.Evaluate(_runningTime);

            yield return null;
        }

        _coroutine = null;
        _action?.Invoke();
    }
}