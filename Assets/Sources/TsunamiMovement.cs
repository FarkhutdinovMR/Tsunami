using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class TsunamiMovement : MonoBehaviour, IMovement
{
    [SerializeField] private Stat _moveSpeed;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private Transform _camera;
    [SerializeField] private Rebound _rebound;

    private CharacterController _characterController;
    private bool _canMove = true;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_canMove == false)
            return;

        Turn(0f);
        Move();
    }

    public void Turn(float delta)
    {
        Vector3 direction = _camera.forward;
        direction.y = 0f;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), _angularSpeed * Time.deltaTime);
    }

    public void Move()
    {
        _characterController.SimpleMove(transform.forward * _moveSpeed.Value);
    }

    public void Rebound(Vector3 position)
    {
        _canMove = false;
        _rebound.Play(position, () => _canMove = true);
    }
}