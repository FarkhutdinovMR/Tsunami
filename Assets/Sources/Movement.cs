using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _angularSpeed = 1;

    private void Update()
    {
        Move();
    }

    public void Turn(float delta)
    {
        transform.Rotate(Vector3.up * delta * _angularSpeed * Time.deltaTime);
    }

    private void Move()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}