using Controllers.UI;
using MB;
using Profile;
using UnityEngine;

namespace Controllers
{
    public class MainController : BaseController
    {
        private ProfilePlayers _profilePlayer;
        private Transform _placeForUi;
        private GameLevel _gameLevel;
        private Player _player;
        private DiamondScanner _diamondScanner;
        private EnemyScanner _enemyScanner;
        private TileMapScanner _tileMapScanner;

        private MainMenuController _mainMenuController;
        private SettingsMenuController _settingsMenuController;
        private VolumeMenuController _volumeMenuController;
        private LanguageMenuController _languageMenuController;
        private GameController _gameController;

        private ExitController _exitController;
        private LevelMenuController _levelMenuController;


        public MainController(ProfilePlayers profilePlayer, Transform placeForUi, GameLevel gameLevel, Player player, DiamondScanner diamondScanner, EnemyScanner enemyScanner, TileMapScanner tileMapScanner)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _gameLevel = gameLevel;
            _player = player;
            _diamondScanner = diamondScanner;
            _enemyScanner = enemyScanner;
            _tileMapScanner = tileMapScanner;

            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);
            
        }

        public void Update()
        {
            _gameController?.Update();
        }
        
        protected override void OnDispose()
        {
        }

        private void OnChangeGameState(GameState state)
        {
            DisposeControllers();
            switch (state)
            {
                case GameState.Game:
                    _gameController = new GameController(_placeForUi, _profilePlayer, _player, _diamondScanner, _enemyScanner, _tileMapScanner);
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
                    _volumeMenuController = new VolumeMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.ExitMenu:
                    _exitController = new ExitController(_placeForUi, _profilePlayer);
                    break;
            }
        }

        private void DisposeControllers()
        {
            _mainMenuController?.Dispose();
            _exitController?.Dispose();
            _levelMenuController?.Dispose();
            _settingsMenuController?.Dispose();
            _languageMenuController?.Dispose();
            _volumeMenuController?.Dispose();
            _gameController?.Dispose();
        }
    }
}
