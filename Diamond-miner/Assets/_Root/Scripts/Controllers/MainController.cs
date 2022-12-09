using Controllers.UI;
using Profile;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class MainController : BaseController
    {
        private ProfilePlayers _profilePlayer;
        private Transform _placeForUi;
        private GameLevel _gameLevel;
        private LevelManager _levelManager;
        private AudioMixer _audioMixer;
        private AudioEffectsManager _audioEffectsManager;
        private AudioSource _audioSource;

        private MainMenuController _mainMenuController;
        private SettingsMenuController _settingsMenuController;
        private VolumeMenuController _volumeMenuController;
        private LanguageMenuController _languageMenuController;

        private ExitController _exitController;
        private LevelMenuController _levelMenuController;
        private FartherMenuController _fartherMenuController;
        private GameOverMenuController _gameOverMenuController;
        private RoomController _roomController;


        public MainController(ProfilePlayers profilePlayer, Transform placeForUi, GameLevel gameLevel, LevelManager levelManager, AudioMixer audioMixer, 
            AudioEffectsManager audioEffectsManager, AudioSource audioSource)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _gameLevel = gameLevel;
            _levelManager = levelManager;
            _audioMixer = audioMixer;
            _audioEffectsManager = audioEffectsManager;
            _audioSource = audioSource;

            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);
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
                    SceneManager.LoadScene(1);
                    break;
                case GameState.MainMenu:
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _audioEffectsManager, _audioSource);
                    break;
                case GameState.LevelMenu:
                    _levelMenuController = new LevelMenuController(_placeForUi, _profilePlayer, _gameLevel, _audioEffectsManager, _audioSource);
                    break;
                case GameState.SettingsMenu:
                    _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer, _audioEffectsManager, _audioSource);
                    break;
                case GameState.LanguageMenu:
                    _languageMenuController = new LanguageMenuController(_placeForUi, _profilePlayer, _audioEffectsManager, _audioSource);
                    break;
                case GameState.VolumeMenu:
                    _volumeMenuController = new VolumeMenuController(_placeForUi, _profilePlayer, _audioMixer, _audioEffectsManager, _audioSource);
                    break;
                case GameState.FartherMenu:
                    _fartherMenuController = new FartherMenuController(_placeForUi, _profilePlayer, _levelManager, _gameLevel, _audioEffectsManager, _audioSource);
                    break;
                case GameState.GameOverMenu:
                    _gameOverMenuController = new GameOverMenuController(_placeForUi, _profilePlayer, _audioEffectsManager, _audioSource);
                    break;
                case GameState.ExitMenu:
                    _exitController = new ExitController(_placeForUi, _profilePlayer, _audioEffectsManager, _audioSource);
                    break;
                case GameState.Room:
                    _roomController = new RoomController(_placeForUi, _profilePlayer, _audioEffectsManager, _audioSource);
                    break;
            }
        }

        private void DisposeControllers()
        {
            _levelMenuController?.Dispose();
            _gameOverMenuController?.Dispose();
            _exitController?.Dispose();
            _settingsMenuController?.Dispose();
            _languageMenuController?.Dispose();
            _volumeMenuController?.Dispose();
            _fartherMenuController?.Dispose();
            _mainMenuController?.Dispose();
            _roomController?.Dispose();


        }
    }
}
