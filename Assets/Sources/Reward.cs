using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private RewardPresenter _rewardPresenter;

    public void Show(uint text, Camera camera, Level level)
    {
        RewardPresenter newReward = Instantiate(_rewardPresenter, transform.position, transform.rotation);
        newReward.Init(text, camera, level);
    }
}