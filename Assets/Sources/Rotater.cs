using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private Vector3 _rotate;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(_rotate);
    }
}