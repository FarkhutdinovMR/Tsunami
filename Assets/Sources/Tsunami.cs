using UnityEngine;

public class Tsunami : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Level _level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            uint reward = obstacle.Destroy(_level.Value);

            if (reward > 0)
                _level.AddExp(reward);
            else
                _movement.Rebound(other.transform);
        }
    }
}