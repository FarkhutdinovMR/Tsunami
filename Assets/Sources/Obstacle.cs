using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private uint _strength = 1;
    [SerializeField] private float _fallSpeed = 1;
    [SerializeField] private float _rotateSpeed = 1;
    [SerializeField] private float _destroyDelay = 1;
    [SerializeField] private uint _reward = 1;

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    public uint Destroy(uint power, Vector3 forcePosition)
    {
        if (power < _strength)
            return 0;

        StartCoroutine(Rotate(forcePosition));
        StartCoroutine(Fall());
        Destroy(gameObject, _destroyDelay);
        _collider.enabled = false;
        OnDestroy2();

        return _reward;
    }

    protected virtual void OnDestroy2() { }

    private IEnumerator Rotate(Vector3 forcePosition)
    {
        Quaternion startRotation = transform.rotation;
        Vector3 fallDirection = (transform.position - forcePosition).normalized;
        fallDirection.y = 0f;
        Quaternion endRotation = Quaternion.LookRotation(transform.forward, fallDirection);
        float evolution = 0f;

        while (evolution < 1f)
        {
            evolution += Time.deltaTime * _rotateSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, evolution);
            yield return null;
        }
    }

    private IEnumerator Fall()
    {
        while (true)
        {
            transform.position += _fallSpeed * Time.deltaTime * Vector3.down;
            yield return null;
        }
    }
}