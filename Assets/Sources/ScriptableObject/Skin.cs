using UnityEngine;

[CreateAssetMenu(fileName = "NewSkin", menuName = "ScriptableObjects/Skin", order = 1)]
public class Skin : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _template;
    [SerializeField] private uint _unlockLevel;

    public string Name => _name;
    public Sprite Icon => _icon;
    public GameObject Template => _template;
    public uint UnlockLevel => _unlockLevel;
}