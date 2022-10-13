using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStat", menuName = "ScriptableObjects/Stat", order = 1)]
public class Stat : ScriptableObject
{
    [SerializeField] private float _value;
    [SerializeField] private string _name;
    [SerializeField] private uint _startCost;
    [SerializeField] private uint _defaultValue;
    [SerializeField] private float _increase;
    [SerializeField] private float _maxValue;
    [SerializeField] private Sprite _icon;

    private uint _upgradeCost;

    public string Name => _name;
    public float Value => _value;
    public uint UpgradeCost => _upgradeCost;
    public uint DefaultValue => _defaultValue;
    public float MaxValue => _maxValue;
    public uint StartCost => _startCost;
    public Sprite Icon => _icon;

    public void Init(float value, uint upgradeCost)
    {
        _value = value;
        _upgradeCost = upgradeCost;
    }

    public bool CanUpgrade => _value + _increase <= _maxValue;

    public void Upgrade()
    {
        if (CanUpgrade == false)
            throw new InvalidOperationException();

        _value += _increase;
        _upgradeCost += _upgradeCost;
    }
}