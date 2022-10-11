using UnityEngine;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField] private UIButton _okButton;
    [SerializeField] private LevelCompletedWindow _level;
    [SerializeField] private CompositeRoot _root;

    private void OnEnable()
    {
        _okButton.Clicked += OnOkButtonClicked;
    }

    private void OnDisable()
    {
        _okButton.Clicked -= OnOkButtonClicked;
    }

    private void OnOkButtonClicked()
    {
        gameObject.SetActive(false);
        _root.LoadNextLevel();
    }
}