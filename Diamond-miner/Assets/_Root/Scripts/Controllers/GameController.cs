using System.Collections.Generic;
using Controllers.UI;
using MB;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{
    internal class GameController : BaseController
    {
        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;
        private Player _player;
        private DiamondScanner _diamondScanner;
        private EnemyScanner _enemyScanner;
        private TileMapScanner _tileMapScanner;
        private LevelManager _levelManager;
        private GameLevel _gameLevel;
        private PauseManager _pauseManager;

        private DiamondController _diamondController;
        private PlayerController _playerController;
        private EnemyController _enemyController;
        private GameUIController _gameUiController;

        private List<GameObject> _emenys;

        private Tilemap _tileMap;

        public GameController(Transform placeForUi, ProfilePlayers profilePlayer, Player player, DiamondScanner diamondScanner, EnemyScanner enemyScanner, TileMapScanner tileMapScanner, LevelManager levelManager, GameLevel gameLevel, PauseManager pauseManager)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _player = player;
            _diamondScanner = diamondScanner;
            _enemyScanner = enemyScanner;
            _tileMapScanner = tileMapScanner;
            _levelManager = levelManager;
            _gameLevel = gameLevel;
            _pauseManager = pauseManager;
            
            LoadLevel();

            _emenys = _enemyScanner.GetEnemy();

            CreateControllers();

        }

        private void CreateControllers()
        {
            _diamondController = new DiamondController(_diamondScanner.GetDiamonds(), _player);
            _playerController = new PlayerController(_player, _tileMap, _diamondController);
            _gameUiController = new GameUIController(_placeForUi, _profilePlayer, _diamondScanner, _diamondController, _pauseManager);
            _enemyController = new EnemyController(_emenys);
        }

        private void LoadLevel()
        {
            var prefab = _levelManager.Levels[_gameLevel.CurrentLevel];
            GameObject objectView = Object.Instantiate(prefab);
            _tileMap = _tileMapScanner.GetTileMap();
            _player.gameObject.SetActive(true);
            _player.gameObject.transform.position = new Vector3(-7.5f, 3.5f, 0f);
        }

        public void Update(float deltaTime)
        {
            _playerController.Update();
            _gameUiController.Update(deltaTime);
        }

        protected override void OnDispose()
        {
            _player.gameObject.SetActive(false);
            _diamondController?.Dispose();
            _playerController?.Dispose();
            _enemyController?.Dispose();
            _gameUiController?.Dispose();
        }
    }
}