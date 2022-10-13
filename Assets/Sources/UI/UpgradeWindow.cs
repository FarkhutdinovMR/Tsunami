using UnityEngine;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField] private UIButton _okButton;
    [SerializeField] private LevelCompletedWindow _level;
    [SerializeField] private CompositeRoot _root;
    [SerializeField] private SkinSelector _skinSelector;

    private void OnEnable()
    {
        _okButton.Clicked += OnOkButtonClicked;
    }

    private void OnDisable()
    {
        _okButton.Clicked -= OnOkButtonClicked;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _skinSelector.Show();
    }

    private void OnOkButtonClicked()
    {
        gameObject.SetActive(false);
        _root.LoadNextLevel();
    }
}