using UnityEngine;
using TMPro;

public class RewardPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void SetReward(uint value)
    {
        _text.SetText("+" + value.ToString());
    }
}