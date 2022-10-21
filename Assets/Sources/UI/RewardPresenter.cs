using UnityEngine;

public class RewardPresenter : MonoBehaviour
{
    [SerializeField] private TextPresenter _textPresenter;
    [SerializeField] private LookAtTarget _lookAtTarget;
    [SerializeField] private UpAnimation _upAnimation;

    public void Init(uint value, Camera camera, Level level)
    {
        _textPresenter.UpdateText(value);
        _lookAtTarget.Init(camera.transform);
        _upAnimation.Init(level);
    }
}