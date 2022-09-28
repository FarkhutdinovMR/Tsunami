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
    }
}