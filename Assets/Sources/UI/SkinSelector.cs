using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TsunamiSkin _skin;
    [SerializeField] private Transform _skinContainer;
    [SerializeField] private SkinPresenter _skinTemplate;
    [SerializeField] private Image _skinIcon;
    [SerializeField] private TMP_Text _name;

    private void Start()
    {
        _skinIcon.sprite = _skin.CurrentSkin.Icon;
        _name.SetText(_skin.CurrentSkin.Name);

        foreach (Skin skin in _skin.Skins)
        {
            SkinPresenter newSkin = Instantiate(_skinTemplate, _skinContainer);
            newSkin.Init(skin, OnSkinButtonClicked, _player.Level < skin.UnlockLevel ? true : false);
        }
    }

    private void OnSkinButtonClicked(Skin skin)
    {
        _skinIcon.sprite = skin.Icon;
        _skin.ChangeSkin(skin);
        _name.SetText(skin.Name);
    }
}