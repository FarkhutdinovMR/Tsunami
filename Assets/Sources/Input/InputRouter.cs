using UnityEngine;
using UnityEngine.InputSystem;

public class InputRouter : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _movementBehaviour;
    [SerializeField] private CompositeRoot _compositeRoot;

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
        _input.Menu.Enable();
        _input.Movement.Disable();
        _input.Menu.Start.performed += OnMenuStartPerformed;
    }

    private void OnDisable()
    {
        Disable();
        _input.Menu.Start.performed -= OnMenuStartPerformed;
    }

    private void Update()
    {
        _movement.Turn(_input.Movement.Turn.ReadValue<float>());
    }

    public void Disable()
    {
        _input.Disable();
    }

    private void OnMenuStartPerformed(InputAction.CallbackContext context)
    {
        _compositeRoot.Resume();
        _input.Menu.Disable();
        _input.Movement.Enable();
    }
}