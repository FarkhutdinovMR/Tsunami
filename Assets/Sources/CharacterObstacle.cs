using UnityEngine;

public class CharacterObstacle : Obstacle
{
    [SerializeField] private Character _character;

    protected override void OnDestroy2()
    {
        _character.Die();
    }
}