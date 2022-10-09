using UnityEngine;

public class Player : MonoBehaviour
{
    private uint _exp;
    private uint _level;

    public uint Level => _level;
    public uint LevelUp => 100;
    public uint Exp => _exp;

    public void Init(uint exp, uint level)
    {
        _exp = exp;
        _level = level;
    }

    public void AddExp(uint value)
    {
        _exp += value;

        if (_exp >= LevelUp * _level)
            _level++;
    }
}