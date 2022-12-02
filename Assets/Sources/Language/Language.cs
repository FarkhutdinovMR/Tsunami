using Agava.YandexGames;
using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    private Dictionary<string, string> _languageISO639_1Codes = new()
    {
        { "ru", "Russian" },
        { "en", "English" },
        { "tr", "Turkish" },
    };

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(WaitSDKInitialize(()=>
        {
            Set(YandexGamesSdk.Environment.i18n.lang);
        }));
#endif
    }

    private IEnumerator WaitSDKInitialize(Action onSDKInitilized)
    {
        while (true)
        {
            if (YandexGamesSdk.IsInitialized)
            {
                onSDKInitilized();
                yield break;
            }

            yield return new WaitForSecondsRealtime(1);
        }
    }

    private void Set(string value)
    {
        if (_languageISO639_1Codes.ContainsKey(value))
            _leanLocalization.SetCurrentLanguage(_languageISO639_1Codes[value]);
    }
}