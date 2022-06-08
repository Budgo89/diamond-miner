using MB;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{
    internal class PlayerController : BaseController
    {
        private Player _player;
        private Tilemap _tileMap;

        private float _xAxisInput;
        
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private Tilemap _tilemap;
        private TilemapController _tilemapController;
        public PlayerController(Player player, Tilemap tileMap)
        {
            _player = player;
            _tileMap = tileMap;
            _tilemapController = new TilemapController(player, tileMap);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(-1,0);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Move(1,0);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(0,1);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(0,-1);
            }
        }

        private void Move(int x, int y)
        {
            if (x != 0)
            {
                
                var flaEdgeMap = _tilemapController.IsEdgeMap(x, Vector2.right);
                if (!flaEdgeMap)
                    return;
                _xAxisInput = Input.GetAxis("Horizontal");
                _player.transform.position = new Vector3(_player.transform.position.x + x,
                    _player.transform.position.y, _player.transform.position.z);
                _player.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
                _tilemapController.RemoveSoil(x, Vector2.right);
            }

            if (y != 0)
            {
                
                var flagEdgeMap = _tilemapController.IsEdgeMap(y, Vector2.up);
                if (!flagEdgeMap)
                    return;
                _player.transform.position = new Vector3(_player.transform.position.x,
                    _player.transform.position.y + y, _player.transform.position.z);
                _tilemapController.RemoveSoil(y, Vector2.up);
            }

        }

        protected override void OnDispose()
        {
            _tilemapController?.Dispose();
        }
    }

}
