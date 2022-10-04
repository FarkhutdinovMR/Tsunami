using System;
using UnityEngine;

public class Tsunami : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private uint _levelup = 5;

    private uint _score;
    private uint _level = 1;

    public event Action<uint> ScoreChanged;
    public event Action<uint> LevelChanged;
    public event Action<uint> RewardGetted;

    public uint Level => _level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            uint reward = obstacle.Destroy(_level);
            if (reward > 0)
                AddScore(reward);
            else
                _movement.Rebound(other.transform);
        }
    }

    private void AddScore(uint value)
    {
        _score += value;
        RewardGetted?.Invoke(value);

        if (_score >= _levelup)
        {
            _level++;
            _levelup += _levelup;
            LevelChanged?.Invoke(_level);
        }

        ScoreChanged?.Invoke(_score);
    }
}