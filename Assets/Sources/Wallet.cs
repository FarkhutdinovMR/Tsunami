using UnityEngine;

public class Wallet : MonoBehaviour
{
    private uint _gold;

    public uint Gold => _gold;

    public void Init(uint gold)
    {
        _gold = gold;
    }

    public void Add(uint gold)
    {
        _gold += gold;
    }

    public bool TryBuy(uint cost)
    {
        if (cost > _gold)
            return false;

        _gold -= cost;
        return true;
    }
}