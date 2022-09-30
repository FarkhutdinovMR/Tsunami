using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _angularSpeed = 1;
    [SerializeField] private AnimationCurve _downRebound;
    [SerializeField] private AnimationCurve _backRebound;
    [SerializeField] private float _reboundSpeed = 1;

    private float _runningTime;
    private Vector3 _previousPosition;
    private bool _canMove = true;

    private void Update()
    {
        if (_canMove)
            Move();
    }

    public void Turn(float delta)
    {
        transform.Rotate(Vector3.up * delta * _angularSpeed * Time.deltaTime);
    }

    public void Rebound(Transform target)
    {
        if (_canMove)
            StartCoroutine(ReboundAnimation(target));
    }

    private void Move()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private IEnumerator ReboundAnimation(Transform target)
    {
        _canMove = false;
        _runningTime = 0;
        _previousPosition = transform.position;
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        direction = direction.normalized;

        while (_runningTime <= 1)
        {
            _runningTime += _reboundSpeed * Time.deltaTime;
            var offset = direction * _backRebound.Evaluate(_runningTime) - Vector3.down *_downRebound.Evaluate(_runningTime);
            transform.position = _previousPosition + offset;

            yield return null;
        }

        _canMove = true;
    }
}