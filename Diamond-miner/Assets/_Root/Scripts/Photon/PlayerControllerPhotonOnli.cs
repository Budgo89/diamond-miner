using System.Collections;
using Controllers;
using MB;
using Photon.Pun;
using TMPro;
using Tool;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using View;

public class PlayerControllerPhotonOnli : MonoBehaviourPunCallbacks, IPunObservable
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
    private PauseManager _pauseManager;

    private Vector2 _position = new Vector2(0, 0);
    private Vector3 _positionPlayer2 = new Vector3();

    private int _countDiamond;
    private int _countDiamondPlayer = 0;

    #region UI
    
    private GameUIOnlineView _gameUIOnlineView;
    private PauseMenuView _pauseMenuView;
    private ResultsMenuView _resultsMenuView;

    private Button _pauseMenuButton;
    private Button _toTheGameButton;
    private Button _mainMenuButton;

    private TMP_Text _diamondPlayer1Text;
    private TMP_Text _diamondPlayer2Text;
    private GameObject _victorys;
    private Image _victoryPlayer1;
    private Image _victoryPlayer2;
    private Button _ok;


    #endregion

    public void Start()
    {
        _photonViewView = GetComponent<PhotonView>();
        
        _player = GetComponent<Player>();
        PlayingField playingField = FindObjectOfType<PlayingField>();
        _tileMap = playingField.gameObject.GetComponent<Tilemap>();
        _levelView = FindObjectOfType<LevelView>();
        _countDiamond = _levelView.DiamondCount;
        _navMeshSurface = FindObjectOfType<NavMeshSurface2d>();
        _swipeDetection = FindObjectOfType<SwipeDetection>();
        _gameUIOnlineView = FindObjectOfType<GameUIOnlineView>();
        _pauseMenuView = FindObjectOfType<PauseMenuView>();
        _resultsMenuView = FindObjectOfType<ResultsMenuView>();
        _swipeDetection.SwipeEvevt += OnSwipe;
        _pauseManager = new PauseManager();
        _diamondController = new DiamondController(_levelView.Diamonds, _player, _navMeshSurface, _pauseManager);
        _diamondController.DiamondRaised += Diamond—heck;
        _tilemapController = new TilemapController(_player, _tileMap, _navMeshSurface);
        _stoneController = new StoneController(_player, _tilemapController, _navMeshSurface);

        UiControl();
    }
    

    #region PlaterController
    
    private void Diamond—heck()
    {
        //_countDiamondPlayer++;
        _photonViewView.RPC("Diamond—heck1", RpcTarget.Others);
    }
    [PunRPC]
    private void Diamond—heck1()
    {
        _countDiamondPlayer++;
    }

    [PunRPC]
    private void Pause()
    {
        StartCoroutine(PauseCoroutine());
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
        if (_pauseManager.IsPause())
            return;

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

            if (_levelView.Diamonds.Count == 0)
            {
                _photonViewView.RPC("Pause", RpcTarget.Others);
                Pause();
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
            _tilemapController.RemoveSoilPlayer2(x, Vector2.right, _positionPlayer2);
            _diamondController.RaiseDiamondPlayer2(x, Vector2.right, new Vector2(_positionPlayer2.x, _positionPlayer2.y));
        }

        if (y != 0)
        {
            _tilemapController.RemoveSoilPlayer2(y, Vector2.up, _positionPlayer2);
            _diamondController.RaiseDiamondPlayer2(y, Vector2.up, new Vector2(_positionPlayer2.x, _positionPlayer2.y));
        }

    }
    private void Move(int x, int y, float xAxisInput = 0)
    {
        if (x != 0)
        {
            var flaEdgeMap = _tilemapController.IsEdgeMap(x, Vector2.right);
            if (!flaEdgeMap)
                return;
            if (_stoneController.IsObstacle(Vector2.right, x, y))
                return;
            _xAxisInput = xAxisInput == 0 ? Input.GetAxis("Horizontal") : xAxisInput;
            _xAxisInput = Input.GetAxis("Horizontal");
            _player.transform.position = new Vector3(_player.transform.position.x + x,
                _player.transform.position.y, _player.transform.position.z);
            _player.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
            _tilemapController.RemoveSoil(x, Vector2.right);
            _diamondController.RaiseDiamond(x, Vector2.right);
        }

        if (y != 0)
        {
            if (_stoneController.IsObstacle(Vector2.up, x, y))
                return;
            var flagEdgeMap = _tilemapController.IsEdgeMap(y, Vector2.up);
            if (!flagEdgeMap)
                return;
            _player.transform.position = new Vector3(_player.transform.position.x,
                _player.transform.position.y + y, _player.transform.position.z);
            _tilemapController.RemoveSoil(y, Vector2.up);
            _diamondController.RaiseDiamond(y, Vector2.up);
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
            _position = (Vector2)stream.ReceiveNext();
            _positionPlayer2 = (Vector3)stream.ReceiveNext();
        }
    }

    IEnumerator PauseCoroutine()
    {
        
        yield return new WaitForSeconds(1);
        ResultsMenu();
        _pauseManager.EnablePause();
        StopCoroutine(PauseCoroutine());
    }

    #endregion

    #region UIController

    private void ResultsMenu()
    {
        _resultsMenuView.gameObject.SetActive(true);
        _victorys.SetActive(true);
        _diamondPlayer1Text.text = _countDiamondPlayer.ToString();
        _diamondPlayer2Text.text = (_countDiamond - _countDiamondPlayer).ToString();

        if (_countDiamondPlayer > _countDiamond - _countDiamondPlayer)
        {
            _victoryPlayer2.gameObject.SetActive(false);


            int victoryPvp = SaveManagement.GetVictoryPvp() + 1;


            SaveManagement.SetVictoryPvp(victoryPvp);

        }
        else
        {
            _victoryPlayer1.gameObject.SetActive(false);
        }

    }
    

    private void UiControl()
    {
        AddButton();
        SubscribeButton();
        _pauseMenuView.gameObject.SetActive(false);
        _resultsMenuView.gameObject.SetActive(false);
    }

    private void AddButton()
    {
        _pauseMenuButton = _gameUIOnlineView.PauseMenuButton;
        _toTheGameButton = _pauseMenuView.ToTheGameButton;
        _mainMenuButton = _pauseMenuView.MainMenuButton;
        _diamondPlayer1Text = _resultsMenuView.DiamondPlayer1Text;
        _diamondPlayer2Text = _resultsMenuView.DiamondPlayer2Text;
        _victorys = _resultsMenuView.Victorys; 
        _victoryPlayer1 = _resultsMenuView.VictoryPlayer1;
        _victoryPlayer2 = _resultsMenuView.VictoryPlayer2;
        _ok = _resultsMenuView.Ok;
    }

    private void SubscribeButton()
    {
        _pauseMenuButton.onClick.AddListener(OnPauseMenuButtonClick); 
        _toTheGameButton.onClick.AddListener(OnTheGameButtonClick);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        _ok.onClick.AddListener(OnMainMenuButtonClick);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
    
    private void OnMainMenuButtonClick()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    private void OnTheGameButtonClick()
    {
        _gameUIOnlineView.gameObject.SetActive(true);
        _pauseMenuView.gameObject.SetActive(false);
    }

    private void OnPauseMenuButtonClick()
    {
        _gameUIOnlineView.gameObject.SetActive(false);
        _pauseMenuView.gameObject.SetActive(true);
    }

    #endregion
}