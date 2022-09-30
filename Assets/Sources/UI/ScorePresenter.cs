using UnityEngine;
using TMPro;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Tsunami _tsunami;


    private void OnEnable()
    {
        _tsunami.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _tsunami.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(uint value)
    {
        _score.SetText(value.ToString());
    }
}