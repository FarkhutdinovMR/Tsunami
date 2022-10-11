using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStat", menuName = "ScriptableObjects/Stat", order = 1)]
public class Stat : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _value;
    [SerializeField] private uint _upgradeCost;
    [SerializeField] private uint _defaultValue;
    [SerializeField] private float _increase;
    [SerializeField] private float _maxValue;
    [SerializeField] private Sprite _icon;

    public string Name => _name;
    public float Value => _value;
    public uint UpgradeCost => _upgradeCost;
    public uint DefaultValue => _defaultValue;
    public float MaxValue => _maxValue;
    public Sprite Icon => _icon;

    public void Init(float value)
    {
        _value = value;
    }

    public bool CanUpgrade => _value + _increase <= _maxValue;

    public void Upgrade()
    {
        if (CanUpgrade == false)
            throw new InvalidOperationException();

        _value += _increase;
    }
}