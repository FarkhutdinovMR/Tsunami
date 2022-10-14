using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Stat _speed;
    [SerializeField] private float _leadSpeed = 0.5f;

    private void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, _target.position, Time.deltaTime * (_speed.Value + _leadSpeed));
        transform.rotation = Quaternion.Euler(0, _target.eulerAngles.y, 0);
    }
}