using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Tsunami _tsunami;
    [SerializeField] private TsunamiSkin _skin;
    [SerializeField] private Ability _speed;
    [SerializeField] private Ability _agility;
    [SerializeField] private Wallet _wallet;

    private const string Exp = "Exp";
    private const string PlayerLevel = "PlayerLevel";
    private const string Level = "Level";
    private const string Score = "Score";
    private const string Speed = "Speed";
    private const string Agility = "Agility";
    private const string Money = "Money";
    private const string Skin = "Skin";
    private const int DefaultLevel = 1;

    public int GetLevel => PlayerPrefs.GetInt(Level, DefaultLevel);

    public void Save()
    {
        PlayerPrefs.SetInt(Exp, (int)_player.Exp);
        PlayerPrefs.SetInt(PlayerLevel, (int)_player.Level);
        PlayerPrefs.SetInt(Level, SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt(Score, PlayerPrefs.GetInt(Score) + (int)_tsunami.Score);
        PlayerPrefs.SetFloat(Speed, _speed.Value);
        PlayerPrefs.SetFloat(Agility, _agility.Value);
        PlayerPrefs.SetInt(Money, (int)_wallet.Gold);
        PlayerPrefs.SetInt(Skin, (int)_skin.CurrentIndex);
    }

    public void Load()
    {
        _player.Init((uint)PlayerPrefs.GetInt(Exp), (uint)PlayerPrefs.GetInt(PlayerLevel));
        _speed.Init(PlayerPrefs.GetFloat(Speed, _speed.DefaultValue));
        _agility.Init(PlayerPrefs.GetFloat(Agility, _agility.DefaultValue));
        _wallet.Init((uint)PlayerPrefs.GetInt(Money));
        _skin.Init((uint)PlayerPrefs.GetInt(Skin));
    }

    [ContextMenu("Reset to default")]
    public void ResetDefault()
    {
        PlayerPrefs.SetInt(Exp, 0);
        PlayerPrefs.SetInt(PlayerLevel, 1);
        PlayerPrefs.SetInt(Level, DefaultLevel);
        PlayerPrefs.SetInt(Score, 0);
        PlayerPrefs.SetFloat(Speed, _speed.DefaultValue);
        PlayerPrefs.SetFloat(Agility, _agility.DefaultValue);
        PlayerPrefs.SetInt(Money, 0);
        PlayerPrefs.SetInt(Skin, 0);
    }
}