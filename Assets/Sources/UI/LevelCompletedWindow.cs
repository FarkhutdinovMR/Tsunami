using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedWindow : MonoBehaviour
{
    [SerializeField] private UIButton _storeButton;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _exp;
    [SerializeField] private UpgradeWindow _upgradeWindow;
    [SerializeField] private UIButton _nextLevelButton;
    [SerializeField] private TMP_Text _reward;
    [SerializeField] private Slider _slider;
    [SerializeField] private CompositeRoot _root;

    private void OnEnable()
    {
        _storeButton.Clicked += OnStoreButtonClicked;
        _nextLevelButton.Clicked += OnNextLevelButtonClicked;
    }

    private void OnDisable()
    {
        _storeButton.Clicked -= OnStoreButtonClicked;
        _nextLevelButton.Clicked -= OnNextLevelButtonClicked;
    }

    public void Show(uint reward, Level level)
    {
        gameObject.SetActive(true);
        _reward.SetText(reward.ToString());

        uint score = level.Exp % level.LevelUp;
        _exp.SetText(score.ToString() + "/" + level.LevelUp.ToString());
        _level.SetText(level.Value.ToString());
        _slider.value = (float)score / level.LevelUp;
    }

    private void OnStoreButtonClicked()
    {
        gameObject.SetActive(false);
        _upgradeWindow.Show();
    }

    private void OnNextLevelButtonClicked()
    {
        _root.LoadNextLevel();
    }
}