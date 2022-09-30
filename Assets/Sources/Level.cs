using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedWindow;
    [SerializeField] private Tsunami _tsunami;
    [SerializeField] private uint _levelToComlete;
    [SerializeField] private uint _timeToStopGame;

    private void OnEnable()
    {
        _tsunami.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _tsunami.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(uint level)
    {
        if (level >= _levelToComlete)
            ComleteLevel();
    }

    private void ComleteLevel()
    {
        _levelCompletedWindow.SetActive(true);
        StartCoroutine(StopGame());
    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(_timeToStopGame);
        Time.timeScale = 0;
    }
}