using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    [SerializeField] private Level _playerLevel;
    [SerializeField] private TsunamiSkin _skin;
    [SerializeField] private Transform _skinContainer;
    [SerializeField] private SkinPresenter _skinTemplate;
    [SerializeField] private Image _skinIcon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _playerLevelText;

    public void Show()
    {
        _skinIcon.sprite = _skin.CurrentSkin.Icon;
        string skinName = LeanLocalization.GetTranslationText(_skin.CurrentSkin.Name, _skin.CurrentSkin.Name);
        _name.SetText(skinName);
        _playerLevelText.SetText(_playerLevel.Value.ToString());

        foreach (Skin skin in _skin.Skins)
        {
            SkinPresenter newSkin = Instantiate(_skinTemplate, _skinContainer);
            newSkin.Init(skin, OnSkinButtonClicked, _playerLevel.Value < skin.UnlockLevel ? true : false);
        }
    }

    private void OnSkinButtonClicked(Skin skin)
    {
        _skinIcon.sprite = skin.Icon;
        _skin.ChangeSkin(skin);
        string skinName = LeanLocalization.GetTranslationText(skin.Name, skin.Name);
        _name.SetText(skinName);
    }
}