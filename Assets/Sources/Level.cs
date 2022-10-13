using System;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private uint _startLevelUp = 5;
    [SerializeField] private UnityEvent<uint> _levelChanged;

    private uint _value = 1;
    private uint _exp;
    private uint _levelUp;

    public event UnityAction<uint> LevelChanged
    {
        add => _levelChanged.AddListener(value);
        remove => _levelChanged.RemoveListener(value);
    }

    public event Action<uint, uint> ExpChanged;

    public uint Exp => _exp;
    public uint Value => _value;
    public uint LevelUp => _levelUp;
    public uint StartLevelUp => _startLevelUp;
    public uint ExpPerLevel => _levelUp / Value;

    private void Awake()
    {
        _levelUp = _startLevelUp;
    }

    public void Init(uint exp, uint value, uint levelUp)
    {
        _exp = exp;
        _value = value;
        _levelUp = levelUp;
    }

    public void AddExp(uint value)
    {
        uint remain = value;

        while(remain > 0)
        {
            _exp++;
            remain--;

            if (_exp >= _levelUp)
            {
                _value++;
                _levelUp += _levelUp;
                _levelChanged?.Invoke(Value);
            }
        }

        ExpChanged?.Invoke(value, _exp);
    }
}