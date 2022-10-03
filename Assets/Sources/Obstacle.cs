using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private uint _strength = 1;
    [SerializeField] private float _fallSpeed = 1;
    [SerializeField] private float _destroyDelay = 1;
    [SerializeField] private uint _reward = 1;

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    public uint Destroy(uint power)
    {
        if (power < _strength)
            return 0;

        StartCoroutine(Fall());
        Destroy(gameObject, _destroyDelay);
        _collider.enabled = false;
        return _reward;
    }

    private IEnumerator Fall()
    {
        while(true)
        {
            transform.position -= Vector3.up * _fallSpeed * Time.deltaTime;
            yield return null;
        }
    }
}