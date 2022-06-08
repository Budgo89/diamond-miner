using MB;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{
    public class MainController : BaseController
    {
        private Transform _placeForUi;
        private Player _player;
        private Tilemap _tileMap;
        private PlayerController _playerController;

        public MainController(Transform placeForUi, Player player, Tilemap tileMap)
        {
            _placeForUi = placeForUi;
            _player = player;
            _tileMap = tileMap;

            _playerController = new PlayerController(_player, _tileMap);
        }

        public void Update()
        {
            _playerController.Update();
        }
    }
}
