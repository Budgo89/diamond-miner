using System.Collections;
using System.Collections.Generic;
using Controllers;
using MB;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using View;

public class PlayerControllerPhotonOnli : MonoBehaviour, IPunObservable
{
    private Tilemap _tileMap;
    private LevelView _levelView;
    private NavMeshSurface2d _navMeshSurface;
    private SwipeDetection _swipeDetection;
    private PhotonView _photonViewView;

    private DiamondController _diamondController;
    private Player _player;

    private float _xAxisInput;

    private Vector3 _leftScale = new Vector3(-1, 1, 1);
    private Vector3 _rightScale = new Vector3(1, 1, 1);

    private TilemapController _tilemapController;
    private StoneController _stoneController;

    private Vector2 _position = new Vector2(0, 0);
    private Vector3 _positionPlayer2 = new Vector3();
    

    public void Start()
    {
        _photonViewView = GetComponent<PhotonView>();
        
        _player = GetComponent<Player>();
        PlayingField playingField = FindObjectOfType<PlayingField>();
        _tileMap = playingField.gameObject.GetComponent<Tilemap>();
        _levelView = FindObjectOfType<LevelView>();
        _navMeshSurface = FindObjectOfType<NavMeshSurface2d>();
        _swipeDetection = FindObjectOfType<SwipeDetection>();
        _swipeDetection.SwipeEvevt += OnSwipe;
        _diamondController = new DiamondController(_levelView.Diamonds, _player, _navMeshSurface);
        _tilemapController = new TilemapController(_player, _tileMap, _navMeshSurface);
        _stoneController = new StoneController(_player, _tilemapController, _navMeshSurface);
    }

    private void OnSwipe(Vector2 direction)
    {
        if (direction == Vector2.up)
            Move(0, 1);
        if (direction == Vector2.down)
            Move(0, -1);
        if (direction == Vector2.left)
            Move(-1, 0, -1);
        if (direction == Vector2.right)
            Move(1, 0, 1);
    }

    public void Update()
    {
        if (_photonViewView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(-1, 0);
                _position = new Vector2(-1, 0);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Move(1, 0);
                _position = new Vector2(1, 0);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(0, 1);
                _position = new Vector2(0, 1);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(0, -1);
                _position = new Vector2(0, -1);
            }

        }
        else
        {
            Move1((int)_position.x, (int)_position.y);
        }

    }

    private void Move1(int x, int y, float xAxisInput = 0)
    {
        if (x != 0)
        {
            //var flaEdgeMap = _tilemapController.IsEdgeMap(x, Vector2.right);
            //if (!flaEdgeMap)
            //    return;
            //if (_stoneController.IsObstacle(Vector2.right, x, y))
            //    return;
            //_xAxisInput = xAxisInput == 0 ? Input.GetAxis("Horizontal") : xAxisInput;
            ////_xAxisInput = Input.GetAxis("Horizontal");
            //_player.transform.position = new Vector3(_player.transform.position.x + x,
            //    _player.transform.position.y, _player.transform.position.z);
            //_player.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
            _tilemapController.RemoveSoilPlayer2(x, Vector2.right, _positionPlayer2);
            //_diamondController.RaiseDiamond(x, Vector2.right);
            //_navMeshSurface.BuildNavMesh();
        }

        if (y != 0)
        {
            //if (_stoneController.IsObstacle(Vector2.up, x, y))
            //    return;
            //var flagEdgeMap = _tilemapController.IsEdgeMap(y, Vector2.up);
            //if (!flagEdgeMap)
            ////    return;
            //_player.transform.position = new Vector3(_player.transform.position.x,
            //    _player.transform.position.y + y, _player.transform.position.z);
            _tilemapController.RemoveSoilPlayer2(y, Vector2.up, _positionPlayer2);
            //_diamondController.RaiseDiamond(y, Vector2.up);
            //_navMeshSurface.BuildNavMesh();
        }

    }
    private void Move(int x, int y, float xAxisInput = 0)
    {
        if (x != 0)
        {
            var flaEdgeMap = _tilemapController.IsEdgeMap(x, Vector2.right);
            if (!flaEdgeMap)
                return;
            //if (_stoneController.IsObstacle(Vector2.right, x, y))
            //    return;
            _xAxisInput = xAxisInput == 0 ? Input.GetAxis("Horizontal") : xAxisInput;
            //_xAxisInput = Input.GetAxis("Horizontal");
            _player.transform.position = new Vector3(_player.transform.position.x + x,
                _player.transform.position.y, _player.transform.position.z);
            _player.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
            _tilemapController.RemoveSoil(x, Vector2.right);
            //_diamondController.RaiseDiamond(x, Vector2.right);
            //_navMeshSurface.BuildNavMesh();
        }

        if (y != 0)
        {
            //if (_stoneController.IsObstacle(Vector2.up, x, y))
            //    return;
            var flagEdgeMap = _tilemapController.IsEdgeMap(y, Vector2.up);
            if (!flagEdgeMap)
                return;
            _player.transform.position = new Vector3(_player.transform.position.x,
                _player.transform.position.y + y, _player.transform.position.z);
            _tilemapController.RemoveSoil(y, Vector2.up);
            //_diamondController.RaiseDiamond(y, Vector2.up);
            //_navMeshSurface.BuildNavMesh();
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            _positionPlayer2 = _player.transform.position;
            stream.SendNext(_position);
            stream.SendNext(_positionPlayer2);
        }
        else
        {
            Debug.Log($" получение {_positionPlayer2}");
            _position = (Vector2)stream.ReceiveNext();
            _positionPlayer2 = (Vector3)stream.ReceiveNext();
        }
    }
}