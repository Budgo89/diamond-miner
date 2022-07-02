using System;
using Controllers.UI;
using MB;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Audio;

namespace Controllers
{
    public class MainController : BaseController
    {
        private ProfilePlayers _profilePlayer;
        private Transform _placeForUi;
        private GameLevel _gameLevel;
        private Player _player;
        private TileMapScanner _tileMapScanner;
        private LevelManager _levelManager;
        private PauseManager _pauseManager;
        private AudioMixer _audioMixer;

        private MainMenuController _mainMenuController;
        private SettingsMenuController _settingsMenuController;
        private VolumeMenuController _volumeMenuController;
        private LanguageMenuController _languageMenuController;
        private GameController _gameController;

        private ExitController _exitController;
        private LevelMenuController _levelMenuController;
        private FartherMenuController _fartherMenuController;
        private GameOverMenuController _gameOverMenuController;


        public MainController(ProfilePlayers profilePlayer, Transform placeForUi, GameLevel gameLevel, Player player, TileMapScanner tileMapScanner, LevelManager levelManager, PauseManager pauseManager, AudioMixer audioMixer)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _gameLevel = gameLevel;
            _player = player;
            _tileMapScanner = tileMapScanner;
            _levelManager = levelManager;
            _pauseManager = pauseManager;
            _audioMixer = audioMixer;

            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);
        }

        private void LoadVolumeAudio()
        {
            var volume = SaveManagement.GetVolume();
            _audioMixer.SetFloat("volume", (float)(Math.Log10(volume) * 20));
        }

        public void Update(float deltaTime)
        {
            _gameController?.Update(deltaTime);
        }
        
        protected override void OnDispose()
        {
            DisposeControllers();
        }

        private void OnChangeGameState(GameState state)
        {
            DisposeControllers();
            switch (state)
            {
                case GameState.Game:
                    _gameController = new GameController(_placeForUi, _profilePlayer, _player, _tileMapScanner, _levelManager, _gameLevel, _pauseManager);
                    break;
                case GameState.MainMenu:
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.LevelMenu:
                    _levelMenuController = new LevelMenuController(_placeForUi, _profilePlayer, _gameLevel);
                    break;
                case GameState.SettingsMenu:
                    _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.LanguageMenu:
                    _languageMenuController = new LanguageMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.VolumeMenu:
                    _volumeMenuController = new VolumeMenuController(_placeForUi, _profilePlayer, _audioMixer);
                    break;
                case GameState.FartherMenu:
                    _fartherMenuController = new FartherMenuController(_placeForUi, _profilePlayer, _levelManager, _gameLevel);
                    break;
                case GameState.GameOverMenu:
                    _gameOverMenuController = new GameOverMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.ExitMenu:
                    _exitController = new ExitController(_placeForUi, _profilePlayer);
                    break;
            }
        }

        private void DisposeControllers()
        {
            _gameOverMenuController?.Dispose();
            _exitController?.Dispose();
            _levelMenuController?.Dispose();
            _settingsMenuController?.Dispose();
            _languageMenuController?.Dispose();
            _volumeMenuController?.Dispose();
            _fartherMenuController?.Dispose();
            
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
        }
    }
}
