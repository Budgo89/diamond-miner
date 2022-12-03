using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using MB;
using PlayFab;
using PlayFab.ClientModels;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Audio;
using static System.Int32;

internal class EntryPoint : MonoBehaviour
{

    [Header("Scene Objects")] 
    [SerializeField] private Transform _placeForUi;

    [Header("Audio")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioEffectsManager _audioEffectsManager;

    [Header("Level Manager")] [SerializeField]
    private LevelManager _levelManager;

    [Header("PlayFab")] 
    [SerializeField] private string _titleId;
    [SerializeField] private string _gameVersion = "dev";
    [SerializeField] private string _authentificationKey = "AUTHENTIFICATION_KEY";

    private ProfilePlayers _profilePlayer;

    private MainController _mainController;

    private GameLevel _gameLevel;

    private bool _isServer = false;

    private string _myPlayFabId;

    private AudioSource _audioSourceEffects;


    private void Start()
    {
        LoadVolumeAudio();
        GetState();
        SaveManagement.SetGameState(0);
        SaveManagement.SetRestart(0);
        _gameLevel = SaveManagement.GetLevels();
        _audioSourceEffects = GameObject.FindGameObjectWithTag("AudioEffects").GetComponent<AudioSource>();
        _mainController = new MainController(_profilePlayer, _placeForUi, _gameLevel, _levelManager, _audioMixer, _audioEffectsManager, _audioSourceEffects);

    }

    private void Update()
    {
        if (!_isServer)
        {
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                _isServer = true;
                PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), result =>
                {
                    _myPlayFabId = result.AccountInfo.PlayFabId;
                    SaveManagement.myPlayFabId = _myPlayFabId;
                }, error =>
                {
                    Debug.LogError($"Something went wrong: {error.Error}");
                });

                PlayFabClientAPI.GetUserData(new GetUserDataRequest()
                {
                    PlayFabId = _myPlayFabId,
                    Keys = null
                }, result => {
                    _gameLevel.AvailableLevel = Parse(result.Data[_gameLevel.AvailableLevelKey].Value);
                    _gameLevel.CurrentLevel = Parse(result.Data[_gameLevel.CurrentLevelKey].Value);
                }, (error) => {
                    Debug.Log("Got error retrieving user data:");
                    Debug.Log(error.GenerateErrorReport());
                });
                
            }
                
        }
    }
    

    private void Awake()
    {
        PlayFabManager();
    }


    private void GetState()
    {
        var state = SaveManagement.GetGameState();
        switch (state)
        {
            case 0:
                _profilePlayer = new ProfilePlayers(GameState.MainMenu);
                break;
            case 1:
                _profilePlayer = new ProfilePlayers(GameState.Game);
                break;
            case 2:
                _profilePlayer = new ProfilePlayers(GameState.LevelMenu);
                break;
            case 3:
                _profilePlayer = new ProfilePlayers(GameState.SettingsMenu);
                break;
            case 4:
                _profilePlayer = new ProfilePlayers(GameState.ExitMenu);
                break;
            case 5:
                _profilePlayer = new ProfilePlayers(GameState.LanguageMenu);
                break;
            case 6:
                _profilePlayer = new ProfilePlayers(GameState.VolumeMenu);
                break;
            case 7:
                _profilePlayer = new ProfilePlayers(GameState.FartherMenu);
                break;
            case 8:
                _profilePlayer = new ProfilePlayers(GameState.GameOverMenu);
                break;
        }
    }

    private void LoadVolumeAudio()
    {
        var volume = SaveManagement.GetVolume();

        _audioMixer.SetFloat("volume", (float)(Math.Log10(volume) * 20));
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
        SaveManagement.SetLevels(_gameLevel);
    }

    private void PlayFabManager()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = _titleId;
        }

        var needCreation = !PlayerPrefs.HasKey(_authentificationKey);
        var id = PlayerPrefs.GetString(_authentificationKey, Guid.NewGuid().ToString());
        if (needCreation)
        {
            PlayerPrefs.SetString(_authentificationKey, id);
        }
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
