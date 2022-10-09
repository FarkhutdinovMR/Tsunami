using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UIButton _okButton;
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private SaveLoad _saveLoad;
    [SerializeField] private LevelCompletedWindow _level;

    [SerializeField] private Ability[] _abilities;
    [SerializeField] private AbilityUpgradePresenter _abilityTemplate;

    [SerializeField] private TsunamiSkin _skin;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _skinContainer;
    [SerializeField] private SkinPresenter _skinTemplate;
    [SerializeField] private Image _skinIcon;

    private List<AbilityUpgradePresenter> _abilityPresenters = new List<AbilityUpgradePresenter>();
    private List<SkinPresenter> _skinPresenters = new List<SkinPresenter>();

    private void Start()
    {
        foreach(Ability ability in _abilities)
        {
            AbilityUpgradePresenter newAbility = Instantiate(_abilityTemplate, _container);
            newAbility.Init(ability, OnBuyButtonClicked);
            _abilityPresenters.Add(newAbility);
        }

        _skinIcon.sprite = _skin.CurrentSkin.Icon;
        foreach (Skin skin in _skin.Skins)
        {
            SkinPresenter newSkin = Instantiate(_skinTemplate, _skinContainer);
            newSkin.Init(skin, OnSkinButtonClicked, _player.Level < skin.UnlockLevel ? true : false);
            _skinPresenters.Add(newSkin);
        }

        UpdateData();
    }

    private void OnEnable()
    {
        _okButton.Clicked += OnOkButtonClicked;
    }

    private void OnDisable()
    {
        _okButton.Clicked -= OnOkButtonClicked;
    }

    private void OnBuyButtonClicked(Ability ability)
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
        foreach (AbilityUpgradePresenter ability in _abilityPresenters)
            ability.UpdateData();

        _goldText.SetText(_wallet.Gold.ToString());
    }

    private void OnOkButtonClicked()
    {
        _saveLoad.Save();
        gameObject.SetActive(false);
        _level.LoadNextLevel();
    }

    private void OnSkinButtonClicked(Skin skin)
    {
        _skinIcon.sprite = skin.Icon;
        _skin.ChangeSkin(skin);
    }
}