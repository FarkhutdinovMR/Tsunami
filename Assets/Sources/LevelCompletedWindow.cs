using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletedWindow : MonoBehaviour
{
    [SerializeField] private UIButton _storeButton;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _exp;
    [SerializeField] private Upgrade _store;
    [SerializeField] private UIButton _nextLevelButton;
    [SerializeField] private TMP_Text _reward;
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;

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

    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = SceneManager.sceneCountInBuildSettings - 1 > currentIndex ? currentIndex + 1 : currentIndex;
        SceneManager.LoadScene(nextIndex);
        Time.timeScale = 1;
    }

    public void Show(uint reward)
    {
        gameObject.SetActive(true);
        _reward.SetText(reward.ToString());
        _level.SetText(_player.Level.ToString());
        uint score = _player.Exp % _player.LevelUp;
        _exp.SetText(score.ToString() + "/" + _player.LevelUp.ToString());
        _slider.value = (float)score / _player.LevelUp;
    }

    private void OnStoreButtonClicked()
    {
        gameObject.SetActive(false);
        _store.gameObject.SetActive(true);
    }

    private void OnNextLevelButtonClicked()
    {
        LoadNextLevel();
    }
}