using UnityEngine;

public class Tsunami : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _movementSource;
    private IMovement _movement => (IMovement)_movementSource;

    [SerializeField] private Level _level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            uint reward = obstacle.Destroy(_level.Value, transform.position);

            if (reward > 0)
                _level.AddExp(reward);
            else
                _movement.Rebound(other.transform.position);
        }
    }

    private void OnValidate()
    {
        if (_movementSource && !(_movementSource is IMovement))
        {
            Debug.LogError(nameof(_movementSource) + " is not implement " + nameof(_movementSource));
            _movementSource = null;
        }
    }
}