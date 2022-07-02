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
    [SerializeField] private TileMapScanner _tileMapScanner;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioEffectsManager _audioEffectsManager;

    [Header("Level Manager")]
    [SerializeField] private LevelManager _levelManager;

    private Player _player;

    private ProfilePlayers _profilePlayer;
    
    private MainController _mainController;

    private GameLevel _gameLevel;

    private PauseManager _pauseManager;

    private void Start()
    {
        LoadVolumeAudio();
    }
    private void Awake()
    {
        GetPlayer();
        _profilePlayer = new ProfilePlayers(GameState.MainMenu);
        _gameLevel = SaveManagement.GetLevels();
        _pauseManager = new PauseManager();
        _mainController = new MainController(_profilePlayer, _placeForUi, _gameLevel, _player, _tileMapScanner, _levelManager, _pauseManager, _audioMixer, _audioEffectsManager);
    }

    private void LoadVolumeAudio()
    {
        var volume = SaveManagement.GetVolume();
       
        _audioMixer.SetFloat("volume", (float)(Math.Log10(volume) * 20));
    }

    private void GetPlayer()
    {
        _player = (FindObjectsOfType(typeof(Player)) as Player[]).FirstOrDefault();
        _player.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
        SaveManagement.SetLevels(_gameLevel);
    }

    public void Update()
    {
        if (_pauseManager.IsPause())
        {
            return;
        }
        var deltaTime = Time.deltaTime;
        _mainController.Update(deltaTime);
    }
}
