using Cinemachine;
using UnityEngine;

[RequireComponent (typeof(CinemachineVirtualCamera))]
public class CameraDistance : MonoBehaviour
{
    [SerializeField] private CharacterController _collider;
    [SerializeField] private AnimationCurve _distance;
    [SerializeField] private AnimationCurve _shoulderOffset;

    private CinemachineVirtualCamera _camera;
    private Cinemachine3rdPersonFollow _cinemachine3RdPersonFollow;

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        CinemachineComponentBase componentBase = _camera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        _cinemachine3RdPersonFollow = componentBase as Cinemachine3rdPersonFollow;
    }

    private void Update()
    {
        _cinemachine3RdPersonFollow.CameraDistance = _distance.Evaluate(_collider.radius);
        _cinemachine3RdPersonFollow.ShoulderOffset.y = _shoulderOffset.Evaluate(_collider.radius);
    }
}