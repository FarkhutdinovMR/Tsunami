using UnityEngine;

public class LevelUpPresenter : MonoBehaviour
{
    [SerializeField] private LookAtTarget _levelUpTemplate;
    [SerializeField] private Tsunami _tsunami;
    [SerializeField] private Transform _target;

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
        LookAtTarget levelUp = Instantiate(_levelUpTemplate, _tsunami.transform.position, _tsunami.transform.rotation, _tsunami.transform);
        levelUp.SetTarget(_target);
    }
}