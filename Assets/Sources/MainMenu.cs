using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIButton _playButton;

    private void OnEnable()
    {
        _playButton.Clicked += OnPlayButtonClicked;
    }

    private void OnDisable()
    {
        _playButton.Clicked -= OnPlayButtonClicked;
    }

    private void Start()
    {
        OpenMenu();
    }

    private void OnPlayButtonClicked()
    {
        StartGame();
    }

    private void OpenMenu()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    private void StartGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}