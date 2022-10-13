using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private SaveLoad _saveLoad;
    [SerializeField] private Stage _stage;

    private void Start()
    {
        _stage.LoadStage(_saveLoad.GetStage);
    }
}