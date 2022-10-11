using UnityEngine;

public class InputRouter : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    private TsunamiInput _input;

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

        if (_input.Movement.Touch.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            float position = _input.Movement.TurnTouch.ReadValue<float>();
            float delta = position < Screen.width * 0.5f ? -1 : 1;
            _movement.Turn(delta);
        }
    }
}