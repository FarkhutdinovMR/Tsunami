using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PhysicMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _agility;

    private Rigidbody _rigidbody;
    private bool _canMove = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_canMove)
            Move();
    }
    public void Turn(float delta)
    {
        transform.Rotate(Vector3.up * delta * _agility * Time.deltaTime);
    }
    public void Move()
    {
        _rigidbody.AddForce(Vector3.forward * _moveSpeed * Time.deltaTime);
    }
}

public interface IMovement
{
    void Turn(float delta);
    void Move();
}