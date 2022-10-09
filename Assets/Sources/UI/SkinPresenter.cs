using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinPresenter : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private UIButton _button;
    [SerializeField] private Image _lockImage;
    [SerializeField] private TMP_Text _level;

    private Skin _skin;
    private Action<Skin> _action;

    private const string Level = "lv";

    private void OnEnable()
    {
        _button.Clicked += OnButtonClicked;
    }

    private void OnDisable()
    {
        _button.Clicked -= OnButtonClicked;
    }

    public void Init(Skin skin, Action<Skin> action, bool _isLock)
    {
        _skin = skin;
        _icon.sprite = skin.Icon;
        _level.SetText(Level + skin.UnlockLevel.ToString());

        if (_isLock)
            _lockImage.gameObject.SetActive(true);
        else
            _action = action;
    }

    private void OnButtonClicked()
    {
        _action?.Invoke(_skin);
    }
}