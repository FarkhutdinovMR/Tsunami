using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed = 1;
    [SerializeField] private SphereCollider _collider;

    private void LateUpdate()
    {
        Vector3 newPosition = _target.position + (_target.forward * (_offset.z - _collider.radius * 2) + Vector3.up * (_offset.y + _collider.radius));
        transform.position = Vector3.Slerp(transform.position, newPosition, _speed * Time.deltaTime);
        transform.LookAt(_target);
    }
}