using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Tsunami _tsunamiTemplate;
    [SerializeField] private Task _taskTemplate;
    [SerializeField] private Camera _camera;

    private Tsunami _tsunami;
    private Task _task;

    private void Start()
    {
        _tsunami = Instantiate(_tsunamiTemplate);
        _task = Instantiate(_task);
        _task.Init(_tsunami);
    }
}