using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    private int _value;

    private const int loaderStage = 1;

    public int Count => SceneManager.sceneCountInBuildSettings - loaderStage;
    public int NextStage => Count > _value ? _value + 1 : _value;

    private void Start()
    {
        _value = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNext()
    {
        _value = NextStage;
        LoadStage(_value);
    }

    public void LoadStage(int value)
    {
        if (value > Count)
            throw new ArgumentOutOfRangeException(nameof(value));

        SceneManager.LoadScene(value);
    }
}