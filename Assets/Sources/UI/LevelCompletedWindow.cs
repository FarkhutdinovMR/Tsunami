using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedWindow : MonoBehaviour
{
    [SerializeField] private UIButton _storeButton;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _exp;
    [SerializeField] private UpgradeWindow _store;
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

    public void Show(uint reward, Player player)
    {
        gameObject.SetActive(true);

        uint score = player.Exp % player.LevelUp;
        _exp.SetText(score.ToString() + "/" + player.LevelUp.ToString());
        _reward.SetText(reward.ToString());
        _level.SetText(player.Level.ToString());
        _slider.value = (float)score / player.LevelUp;
    }

    private void OnStoreButtonClicked()
    {
        gameObject.SetActive(false);
        _store.gameObject.SetActive(true);
    }

    private void OnNextLevelButtonClicked()
    {
        _root.LoadNextLevel();
    }
}