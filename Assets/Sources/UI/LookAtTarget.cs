using System;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _scale;

    private void Update()
    {
        if (_target != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _target.position);
            float distance = Vector3.Distance(transform.position, _target.position);
            transform.localScale = distance / _scale * Vector3.one;
        }
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));

        _target = target;
    }
}