using UnityEngine;

public interface IMovement
{
    void Move();
    void Turn(float delta);
    void Rebound(Vector3 position);
}