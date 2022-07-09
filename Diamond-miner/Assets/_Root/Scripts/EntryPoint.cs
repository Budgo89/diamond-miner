using System;
using System.Linq;
using Controllers;
using MB;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Audio;

internal class EntryPoint : MonoBehaviour
{

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AudioMixer _audioMixer;

    [Header("Level Manager")]
    [SerializeField] private LevelManager _levelManager;
    
    private ProfilePlayers _profilePlayer;
    
    private MainController _mainController;

    private GameLevel _gameLevel;

    private void Start()
    {
        LoadVolumeAudio();
    }
    private void Awake()
    {
        GetState();
        SaveManagement.SetGameState(0);
        SaveManagement.SetRestart(0);
        _gameLevel = SaveManagement.GetLevels();
        _mainController = new MainController(_profilePlayer, _placeForUi, _gameLevel, _levelManager, _audioMixer);
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
    
}
