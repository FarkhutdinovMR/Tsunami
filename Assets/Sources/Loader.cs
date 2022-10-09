using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField] private SaveLoad _saveLoad;

    private void Start()
    {
        SceneManager.LoadScene(_saveLoad.GetLevel);
    }
}