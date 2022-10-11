using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Stat[] _stats;
    [SerializeField] private StatUpgradePresenter _statPresenterTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private TMP_Text _goldText;

    private List<StatUpgradePresenter> _statsPresenters = new List<StatUpgradePresenter>();

    private void Start()
    {
        foreach (Stat state in _stats)
        {
            StatUpgradePresenter newStatePresenter = Instantiate(_statPresenterTemplate, _container);
            newStatePresenter.Init(state, OnBuyButtonClicked);
            _statsPresenters.Add(newStatePresenter);
        }

        UpdateData();
    }

    private void OnBuyButtonClicked(Stat ability)
    {
        if (ability.CanUpgrade == false)
            return;

        if (_wallet.TryBuy(ability.UpgradeCost) == false)
            return;

        ability.Upgrade();
        UpdateData();
    }

    private void UpdateData()
    {
        foreach (StatUpgradePresenter ability in _statsPresenters)
            ability.UpdateData();

        _goldText.SetText(_wallet.Gold.ToString());
    }
}