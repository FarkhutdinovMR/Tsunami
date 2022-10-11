using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class StatUpgradePresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _currentValue;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private UIButton _buyButton;
    [SerializeField] private Image _icon;

    private Stat _ability;
    private Action<Stat> _buy;

    private void OnEnable()
    {
        _buyButton.Clicked += OnBuyButtonClicked;
    }

    private void OnDisable()
    {
        _buyButton.Clicked -= OnBuyButtonClicked;
    }

    public void Init(Stat ability, Action<Stat> action)
    {
        _ability = ability;
        _buy = action;
        UpdateData();
    }

    public void UpdateData()
    {
        _name.SetText(_ability.Name);
        _currentValue.SetText(_ability.Value.ToString() + "/" + _ability.MaxValue.ToString());
        _cost.SetText(_ability.UpgradeCost.ToString());
        _icon.sprite = _ability.Icon;
    }

    private void OnBuyButtonClicked()
    {
        _buy(_ability);
    }
}