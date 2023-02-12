using Cinemachine;
using UnityEngine;

public class FramingTransposer : MonoBehaviour
{
    [SerializeField] private CharacterController _collider;
    [SerializeField] private AnimationCurve _distance;
    [SerializeField] private AnimationCurve _shoulderOffset;

    private CinemachineVirtualCamera _camera;
    private CinemachineFramingTransposer _cinemachineFramingTransposer;

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        CinemachineComponentBase componentBase = _camera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        _cinemachineFramingTransposer = componentBase as CinemachineFramingTransposer;
    }

    private void Update()
    {
        _cinemachineFramingTransposer.m_CameraDistance = _distance.Evaluate(_collider.radius);
        _cinemachineFramingTransposer.m_TrackedObjectOffset.y = _shoulderOffset.Evaluate(_collider.radius);
    }
}