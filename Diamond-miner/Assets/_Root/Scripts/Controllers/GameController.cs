using System.Collections.Generic;
using System.Linq;
using MB;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{
    internal class GameController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;
        private Player _player;
        private DiamondScanner _diamondScanner;
        private EnemyScanner _enemyScanner;
        private TileMapScanner _tileMapScanner;

        private DiamondController _diamondController;
        private PlayerController _playerController;
        private EnemyController _enemyController;

        private List<GameObject> _emenys;


        private Tilemap _tileMap;

        public GameController(Transform placeForUi, ProfilePlayers profilePlayer, Player player, DiamondScanner diamondScanner, EnemyScanner enemyScanner, TileMapScanner tileMapScanner)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _player = player;
            _diamondScanner = diamondScanner;
            _enemyScanner = enemyScanner;
            _tileMapScanner = tileMapScanner;
            _tileMap = _tileMapScanner.GetTileMap();
            _player.gameObject.SetActive(true);
            _diamondController = new DiamondController(_diamondScanner.GetDiamonds(), _player);



            _playerController = new PlayerController(_player, _tileMap, _diamondController);

            _emenys = _enemyScanner.GetEnemy();
            _enemyController = new EnemyController(_emenys);
            
        }

        public void Update()
        {
            _playerController.Update();
        }
    }
}