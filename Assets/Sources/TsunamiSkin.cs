using System;
using System.Collections;
using UnityEngine;

public class TsunamiSkin : MonoBehaviour
{
    [SerializeField] private Skin[] _skins;
    [SerializeField] private Transform _parent;

    private uint _currentIndex;

    public uint CurrentIndex => _currentIndex;
    public Skin CurrentSkin => _skins[_currentIndex];
    public IEnumerable Skins => _skins;

    public void Init(uint skin)
    {
        if (skin >= _skins.Length)
            throw new IndexOutOfRangeException(nameof(skin));

        _currentIndex = skin;
        Instantiate(_skins[_currentIndex].Template, _parent);
    }

    public void ChangeSkin(Skin skin)
    {
        int index = Array.FindIndex(_skins, s => s == skin);

        if (index == -1)
            throw new IndexOutOfRangeException(nameof(skin));

        _currentIndex = (uint)index;
    }
}