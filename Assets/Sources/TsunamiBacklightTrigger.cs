using UnityEngine;

public class TsunamiBacklightTrigger : MonoBehaviour
{
    [SerializeField] private Tsunami _tsunami;
    [SerializeField] private float _distance;

    private void Update()
    {
        transform.position = _tsunami.transform.position + _tsunami.transform.forward * _distance;
    }
}