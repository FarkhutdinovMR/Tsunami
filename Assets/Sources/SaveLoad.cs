using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private Level _playerLevel;
    [SerializeField] private Tsunami _tsunami;
    [SerializeField] private TsunamiSkin _skin;
    [SerializeField] private Stat _speed;
    [SerializeField] private Stat _agility;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Stage _stage;

    private const string Exp = "Exp";
    private const string PlayerLevel = "PlayerLevel";
    private const string PlayerLevelUp = "PlayerLevelUp";
    private const string Stage = "Stage";
    private const string Speed = "Speed";
    private const string SpeedUpgradeCost = "SpeedUpgradeCost";
    private const string Agility = "Agility";
    private const string AgilityUpgradeCost = "AgilityUpgradeCost";
    private const string Money = "Money";
    private const string Skin = "Skin";
    private const int DefaultLevel = 1;

    public int GetStage => PlayerPrefs.GetInt(Stage, DefaultLevel);

    public void Save()
    {
        PlayerPrefs.SetInt(PlayerLevel, (int)_playerLevel.Value);
        PlayerPrefs.SetInt(PlayerLevelUp, (int)_playerLevel.LevelUp);
        PlayerPrefs.SetInt(Exp, (int)_playerLevel.Exp);
        PlayerPrefs.SetInt(Stage, _stage.NextStage);
        PlayerPrefs.SetFloat(Speed, _speed.Value);
        PlayerPrefs.SetInt(SpeedUpgradeCost, (int)_speed.UpgradeCost);
        PlayerPrefs.SetFloat(Agility, _agility.Value);
        PlayerPrefs.SetInt(AgilityUpgradeCost, (int)_agility.UpgradeCost);
        PlayerPrefs.SetInt(Money, (int)_wallet.Gold);
        PlayerPrefs.SetInt(Skin, (int)_skin.CurrentIndex);
    }

    public void Load()
    {
        _playerLevel.Init((uint)PlayerPrefs.GetInt(Exp), (uint)PlayerPrefs.GetInt(PlayerLevel), (uint)PlayerPrefs.GetInt(PlayerLevelUp));
        _speed.Init(PlayerPrefs.GetFloat(Speed, _speed.DefaultValue), (uint)PlayerPrefs.GetInt(SpeedUpgradeCost));
        _agility.Init(PlayerPrefs.GetFloat(Agility, _agility.DefaultValue), (uint)PlayerPrefs.GetInt(AgilityUpgradeCost));
        _wallet.Init((uint)PlayerPrefs.GetInt(Money));
        _skin.Init((uint)PlayerPrefs.GetInt(Skin));
    }

    [ContextMenu("Reset to default")]
    public void ResetDefault()
    {
        PlayerPrefs.SetInt(PlayerLevelUp, (int)_playerLevel.StartLevelUp);
        PlayerPrefs.SetInt(PlayerLevel, DefaultLevel);
        PlayerPrefs.SetInt(Exp, 0);
        PlayerPrefs.SetInt(Stage, DefaultLevel);
        PlayerPrefs.SetFloat(Speed, _speed.DefaultValue);
        PlayerPrefs.SetInt(SpeedUpgradeCost, (int)_speed.StartCost);
        PlayerPrefs.SetFloat(Agility, _agility.DefaultValue);
        PlayerPrefs.SetInt(AgilityUpgradeCost, (int)_agility.StartCost);
        PlayerPrefs.SetInt(Money, 0);
        PlayerPrefs.SetInt(Skin, 0);
    }
}