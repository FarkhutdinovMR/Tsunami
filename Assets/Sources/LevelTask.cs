using UnityEngine;

public class LevelTask : MonoBehaviour
{
    [SerializeField] private uint _levelToComlete;

    private bool _isComplited;

    public bool IsComplited => _isComplited;

    public void UpdateData(uint level)
    {
        if (level < _levelToComlete)
            return;

        _isComplited = true;
    }
}