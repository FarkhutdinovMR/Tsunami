using TMPro;
using UnityEngine;

public class LevelPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _level;
    [SerializeField] private Tsunami _tsunami;

    private const string levelText = "Level:";

    private void OnEnable()
    {
        _tsunami.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _tsunami.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(uint value)
    {
        _level.SetText(levelText + value.ToString());
    }
}