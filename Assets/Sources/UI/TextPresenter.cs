using UnityEngine;
using TMPro;

public class TextPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _prefix;

    public void UpdateText(uint value)
    {
        _text.SetText(_prefix + value.ToString());
    }
}