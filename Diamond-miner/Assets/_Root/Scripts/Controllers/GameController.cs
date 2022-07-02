﻿using System.Collections.Generic;
using Controllers.UI;
using MB;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using View;

namespace Controllers
{
    internal class GameController : BaseController
    {
        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;
        private Player _player;
        private TileMapScanner _tileMapScanner;
        private LevelManager _levelManager;
        private GameLevel _gameLevel;
        private PauseManager _pauseManager;

        private DiamondController _diamondController;
        private PlayerController _playerController;
        private GameUIController _gameUiController;
        private GameOverController _gameOverController;

        private List<GameObject> _emenys;

        private Tilemap _tileMap;

        private GameObject _level;
        private LevelView _levelView;

        private NavMeshSurface2d _navMeshSurface;

        public GameController(Transform placeForUi, ProfilePlayers profilePlayer, Player player, TileMapScanner tileMapScanner, LevelManager levelManager, GameLevel gameLevel, PauseManager pauseManager)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _player = player;
            _tileMapScanner = tileMapScanner;
            _levelManager = levelManager;
            _gameLevel = gameLevel;
            _pauseManager = pauseManager;
            _pauseManager.DisablePause();

            _level = LoadLevel();
            _levelView = _level.GetComponent<LevelView>();
            _navMeshSurface = _levelView.NavMeshSurface;
            _navMeshSurface.BuildNavMesh();
            CreateControllers();

        }

        private void CreateControllers()
        {
            _diamondController = new DiamondController(_levelView.Diamonds, _player, _navMeshSurface);
            _playerController = new PlayerController(_player, _tileMap, _diamondController, _navMeshSurface);
            _gameUiController = new GameUIController(_placeForUi, _profilePlayer, _levelView.DiamondCount, _diamondController, _pauseManager);
            _gameOverController = new GameOverController(_profilePlayer, _player, _levelView.DiamondCount, _diamondController, _pauseManager, _levelView.Enemys);
        }

        private GameObject LoadLevel()
        {
            var prefab = _levelManager.Levels[_gameLevel.CurrentLevel];
            GameObject objectView = Object.Instantiate(prefab);
            Debug.Log("Создали");
            _tileMap = _tileMapScanner.GetTileMap();
            _player.gameObject.SetActive(true);
            _player.gameObject.transform.position = new Vector3(-7.5f, 3.5f, 0f);
            return objectView;
        }

        public void Update(float deltaTime)
        {
            _playerController.Update();
            _gameUiController.Update(deltaTime);
        }

        protected override void OnDispose()
        {
            Object.Destroy(_level);
            Debug.Log("Уничножели");
            _diamondController?.Dispose();
            _playerController?.Dispose();
            _gameUiController?.Dispose();
            _gameOverController?.Dispose();
            _player?.gameObject.SetActive(false);
            
        }
    }
}