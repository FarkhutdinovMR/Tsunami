using System.Collections;
using UnityEngine;

public class Task : MonoBehaviour
{
    [SerializeField] private LevelCompletedWindow _levelCompletedWindow;
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private SaveLoad _saveLoad;
    [SerializeField] private Tsunami _tsunami;
    [SerializeField] private uint _levelToComlete;
    [SerializeField] private float _timeToStopGame;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private uint goldPerScore;
    [SerializeField] private Player _player;

    private void Start()
    {
        _saveLoad.Load();
    }

    private void OnEnable()
    {
        _tsunami.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _tsunami.LevelChanged -= OnLevelChanged;
    }

    public void Init(Tsunami tsunami)
    {
        _tsunami = tsunami;
    }

    private void OnLevelChanged(uint level)
    {
        if (level >= _levelToComlete)
            CompleteLevel();
    }

    private void CompleteLevel()
    {
        uint reward = _tsunami.Score / goldPerScore;
        _wallet.Add(reward);
        _player.AddExp(_tsunami.Score);
        _saveLoad.Save();
        _levelCompletedWindow.Show(reward);
        _gameMenu.SetActive(false);
        StartCoroutine(StopGame());
    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(_timeToStopGame);
        Time.timeScale = 0;
    }
}