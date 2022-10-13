using System;
using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    [SerializeField] private Stat _moveSpeed;
    [SerializeField] private Stat _agility;
    [SerializeField] private Rebound _rebound;

    private bool _canMove = true;

    private void Update()
    {
        if (_canMove)
            Move();
    }

    public void Turn(float delta)
    {
        transform.Rotate(Vector3.up * delta * _agility.Value * Time.deltaTime);
    }

    public void Move()
    {
        transform.position += transform.forward * _moveSpeed.Value * Time.deltaTime;
    }

    public void Rebound(Transform obstacle)
    {
        if (obstacle == null)
            throw new ArgumentNullException(nameof(obstacle));

        Stop();
        _rebound.Play(obstacle, Resume);
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