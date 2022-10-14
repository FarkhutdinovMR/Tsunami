using TMPro;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private SaveLoad _saveLoad;
    [SerializeField] private Stage _stage;
    [SerializeField] private TMP_Text _version;

    private void Start()
    {
        _saveLoad.ResetDefault();
        _version.SetText("Alpha " + Application.version);
        _stage.LoadStage(_saveLoad.GetStage);
    }
}