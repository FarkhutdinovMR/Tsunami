using System;
using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    [SerializeField] private Stat _moveSpeed;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private Rebound _rebound;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private Transform _chaser;
    [SerializeField] private float _chaserDistance;
    [SerializeField] private Transform _chaserTarget;

    private bool _canMove = true;
    private float yRotation;

    private void Update()
    {
        _cameraTarget.rotation = Quaternion.Euler(_cameraTarget.eulerAngles.x, yRotation, 0);

        if (_canMove)
            Move();
    }

    public void Turn(float delta)
    {
        yRotation += delta * _angularSpeed;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void Move()
    {
        float distance = Vector3.Distance(_chaserTarget.position, _chaser.position);

        if (distance < _chaserDistance)
            transform.position += transform.forward * _moveSpeed.Value * Time.deltaTime;
    }

    public void Rebound(Vector3 position)
    {
        Stop();
        _rebound.Play(position, Resume);
    }

    private void Stop()
    {
        _canMove = false;
    }

    private void Resume()
    {
        _canMove = true;
    }
}