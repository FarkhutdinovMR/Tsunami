using UnityEngine;

public class UpAnimation : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private AnimationCurve _height;

    private Vector3 _startPosition;
    private Vector3 _startBackDirection;
    private Level _level;

    private void Update()
    {
        float y = _height.Evaluate(_level.Value);
        transform.position = Vector3.Lerp(transform.position, _startPosition + Vector3.up * y + _startBackDirection * y * 0.5f, _speed * Time.deltaTime);
    }

    public void Init(Level level)
    {
        _startPosition = transform.position;
        _startBackDirection = -transform.forward;
        _level = level;
    }
}