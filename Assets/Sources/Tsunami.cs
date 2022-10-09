using System;
using UnityEngine;
using UnityEngine.Events;

public class Tsunami : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private uint _levelup = 5;
    [SerializeField] private UnityEvent<uint> _levelChanged;

    private uint _score;
    private uint _level = 1;

    public event UnityAction<uint> LevelChanged
    {
        add => _levelChanged.AddListener(value);
        remove => _levelChanged.RemoveListener(value);
    }

    public event Action<uint> ScoreChanged;
    public event Action<uint> RewardGetted;

    public uint Level => _level;
    public uint Score => _score;

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
            _levelChanged?.Invoke(_level);
        }

        ScoreChanged?.Invoke(_score);
    }
}