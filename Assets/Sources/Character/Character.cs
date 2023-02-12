using UnityEngine;

[RequireComponent (typeof(Animator), typeof(CapsuleCollider))]
public class Character : MonoBehaviour
{
    [SerializeField] private Transform _danger;
    [SerializeField] private float _visibilityRange = 10f;
    [SerializeField] private float _runSpeed = 3f;

    private Animator _animator;
    private CapsuleCollider _collider;
    private State _state;
    private const string WaterTag = "Water";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        switch (_state)
        {
            case State.Idle:
                Idle();
                break;

            case State.Run:
                Run();
                break;
        }
    }

    public void Die()
    {
        _animator.enabled = false;
        enabled = false;
    }

    private void Idle()
    {
        if (Vector3.Distance(transform.position, _danger.position) <= _visibilityRange)
        {
            _state = State.Run;
            _animator.SetTrigger(AnimatorCharacterController.Params.Run);
        }
    }

    private void Run()
    {
        Vector3 direction = (transform.position - _danger.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position += _runSpeed * Time.deltaTime * transform.forward;
        RunAnimation();
    }

    private void RunAnimation()
    {
        _animator.SetBool(AnimatorCharacterController.Params.Swim, IsWater());
    }

    private bool IsWater()
    {
        if (Physics.SphereCast(transform.position + Vector3.up * _collider.height, _collider.radius, Vector3.down, out RaycastHit hit))
            return hit.transform.CompareTag(WaterTag);

        return false;
    }
}

public enum State
{
    Idle,
    Run
}