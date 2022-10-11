using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private TextPresenter _rewardTemplate;

    public void Show(uint value, Transform lookAtTarget)
    {
        TextPresenter newReward = Instantiate(_rewardTemplate, transform.position, transform.rotation);
        newReward.UpdateText(value);
        newReward.GetComponent<LookAtTarget>().Init(lookAtTarget);
    }
}