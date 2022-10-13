using UnityEngine;

public class InputRouter : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _movementBehaviour;

    private TsunamiInput _input;

    private IMovement _movement => (IMovement)_movementBehaviour;

    private void OnValidate()
    {
        if (_movementBehaviour is IMovement)
            return;

        Debug.LogError(_movementBehaviour.name + " needs to implement " + nameof(IMovement));
        _movementBehaviour = null;
    }

    private void OnEnable()
    {
        _input = new();
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        _movement.Turn(_input.Movement.Turn.ReadValue<float>());
    }
}