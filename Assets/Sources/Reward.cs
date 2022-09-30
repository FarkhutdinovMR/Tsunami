using UnityEngine;
using UnityEngine.Animations;

public class Reward : MonoBehaviour
{
    [SerializeField] private RewardPresenter _rewardTemplate;
    [SerializeField] private Tsunami _tsunami;
    [SerializeField] private Transform _lookAtTarget;

    private void OnEnable()
    {
        _tsunami.RewardGetted += OnRewardGetted;
    }

    private void OnDisable()
    {
        _tsunami.RewardGetted -= OnRewardGetted;
    }

    private void OnRewardGetted(uint value)
    {
        RewardPresenter newReward = Instantiate(_rewardTemplate, _tsunami.transform.position, _tsunami.transform.rotation);
        newReward.SetReward(value);
        newReward.GetComponent<LookAtTarget>().SetTarget(_lookAtTarget);
    }
}