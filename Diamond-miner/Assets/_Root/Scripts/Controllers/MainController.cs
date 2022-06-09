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
        private DiamondScanner _diamondScanner;
        private DiamondController _diamondController;

        private PlayerController _playerController;
        

        public MainController(Transform placeForUi, Player player, Tilemap tileMap, DiamondScanner diamondScanner)
        {
            _placeForUi = placeForUi;
            _player = player;
            _tileMap = tileMap;
            _diamondScanner = diamondScanner;
            
            _diamondController = new DiamondController(_diamondScanner.GetDiamonds(), _player);

            _playerController = new PlayerController(_player, _tileMap, _diamondController);
        }

        public void Update()
        {
            _playerController.Update();
        }

        protected override void OnDispose()
        {
            _diamondController?.Dispose();
            _playerController?.Dispose();
        }
    }
}
