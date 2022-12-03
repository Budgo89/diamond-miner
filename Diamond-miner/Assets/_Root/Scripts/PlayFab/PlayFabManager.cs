using System;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField] private string _titleId;
    [SerializeField] private string _gameVersion = "dev";
    [SerializeField] private string _authentificationKey = "AUTHENTIFICATION_KEY";

    void Awake()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = _titleId;
        }

        var needCreation = !PlayerPrefs.HasKey(_authentificationKey);
        var id = PlayerPrefs.GetString(_authentificationKey, Guid.NewGuid().ToString());

        var request = new LoginWithCustomIDRequest
        {
            CustomId = id,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made successful API call!");
    }
    private void OnLoginFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
    }
}

