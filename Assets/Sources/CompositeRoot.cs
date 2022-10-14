using System.Collections;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private LevelCompletedWindow _levelCompletedWindow;
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private SaveLoad _saveLoad;
    [SerializeField] private Tsunami _tsunami;
    [SerializeField] private float _timeToStopGame;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private uint goldPerScore;
    [SerializeField] private Level _tsunamiLevel;
    [SerializeField] private Level _playerLevel;
    [SerializeField] private LevelTask _task;
    [SerializeField] private Size _size;
    [SerializeField] private Reward _reward;
    [SerializeField] private Camera _camera;
    [SerializeField] private TextPresenter _tsunamiLevelPresenter;
    [SerializeField] private TextPresenter _scorePresenter;
    [SerializeField] private GameObject _tsunamiLevelCanvas;
    [SerializeField] private Stage _stage;

    private void Start()
    {
        _saveLoad.Load();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _tsunamiLevel.LevelChanged += OnTsunamiLevelChanged;
        _tsunamiLevel.ExpChanged += OnExpChanged;
    }

    private void OnDisable()
    {
        _tsunamiLevel.LevelChanged -= OnTsunamiLevelChanged;
        _tsunamiLevel.ExpChanged -= OnExpChanged;
    }

    public void LoadNextLevel()
    {
        _saveLoad.Save();
        _stage.LoadNext();
        Time.timeScale = 1;
    }

    private void CompleteGame()
    {
        uint gold = _tsunamiLevel.Exp / goldPerScore;
        _wallet.Add(gold);

        _playerLevel.AddExp(_tsunamiLevel.Exp);
        _levelCompletedWindow.Show(gold, _playerLevel);
        _gameMenu.SetActive(false);
        _tsunamiLevelCanvas.SetActive(false);
        StartCoroutine(StopGame());
        _saveLoad.Save();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

    private void OnExpChanged(uint value, uint value2)
    {
        _reward.Show(value, _camera.transform);
        _scorePresenter.UpdateText(value2);
    }
}