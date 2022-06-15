using System.Collections.Generic;
using Controllers.UI;
using MB;
using Profile;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{
    public class MainController : BaseController
    {
        private ProfilePlayers _profilePlayer;
        private Transform _placeForUi;
        private GameLevel _gameLevel;
        private Player _player;
        private Tilemap _tileMap;
        private DiamondScanner _diamondScanner;
        private EnemyScanner _enemyScanner;
        
        private List<GameObject> _emenys;

        private DiamondController _diamondController;
        private MainMenuController _mainMenuController;
        private PlayerController _playerController;
        private EnemyController _enemyController;

        private ExitController _exitController;
        private LevelMenuController _levelMenuController;


        public MainController(ProfilePlayers profilePlayer, Transform placeForUi, GameLevel gameLevel, Player player, Tilemap tileMap, DiamondScanner diamondScanner, EnemyScanner enemyScanner)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _gameLevel = gameLevel;
            _player = player;
            _tileMap = tileMap;
            _diamondScanner = diamondScanner;
            _enemyScanner = enemyScanner;

            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);

            //_diamondController = new DiamondController(_diamondScanner.GetDiamonds(), _player);

            //_playerController = new PlayerController(_player, _tileMap, _diamondController);
            
            //_emenys = _enemyScanner.GetEnemy();
            //_enemyController = new EnemyController(_emenys);
        }

        public void Update()
        {
            //_playerController.Update();
        }
        
        protected override void OnDispose()
        {
            _diamondController?.Dispose();
            _playerController?.Dispose();
        }

        private void OnChangeGameState(GameState state)
        {
            DisposeControllers();
            switch (state)
            {
                case GameState.MainMenu:
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.LevelMenu:
                    _levelMenuController = new LevelMenuController(_placeForUi, _profilePlayer, _gameLevel);
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
        }
    }
}
