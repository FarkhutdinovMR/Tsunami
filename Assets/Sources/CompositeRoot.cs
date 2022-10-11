using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private LevelCompletedWindow _levelCompletedWindow;
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private SaveLoad _saveLoad;
    [SerializeField] private Tsunami _tsunami;
    [SerializeField] private float _timeToStopGame;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private uint goldPerScore;
    [SerializeField] private Player _player;
    [SerializeField] private LevelTask _task;
    [SerializeField] private Size _size;
    [SerializeField] private Reward _reward;
    [SerializeField] private Camera _camera;
    [SerializeField] private TextPresenter _tsunamiLevelPresenter;
    [SerializeField] private TextPresenter _scorePresenter;

    private void Start()
    {
        _saveLoad.Load();
    }

    private void OnEnable()
    {
        _tsunami.LevelChanged += OnTsunamiLevelChanged;
        _tsunami.RewardGetted += OnRewardGetted;
    }

    private void OnDisable()
    {
        _tsunami.LevelChanged -= OnTsunamiLevelChanged;
        _tsunami.RewardGetted -= OnRewardGetted;
    }

    public void LoadNextLevel()
    {
        _saveLoad.Save();
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = SceneManager.sceneCountInBuildSettings - 1 > currentIndex ? currentIndex + 1 : currentIndex;
        SceneManager.LoadScene(nextIndex);
        Time.timeScale = 1;
    }

    private void CompleteGame()
    {
        uint reward = _tsunami.Score / goldPerScore;
        _wallet.Add(reward);
        _player.AddExp(_tsunami.Score);
        _saveLoad.Save();
        _levelCompletedWindow.Show(reward, _player);
        _gameMenu.SetActive(false);
        StartCoroutine(StopGame());
    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(_timeToStopGame);
        Time.timeScale = 0;
    }

    private void OnTsunamiLevelChanged(uint level)
    {
        _task.UpdateData(level);
        _size.Grow(level);
        _tsunamiLevelPresenter.UpdateText(level);

        if (_task.IsComplited)
            CompleteGame();
    }

    private void OnRewardGetted(uint value)
    {
        _reward.Show(value, _camera.transform);
        _scorePresenter.UpdateText(value);
    }
}