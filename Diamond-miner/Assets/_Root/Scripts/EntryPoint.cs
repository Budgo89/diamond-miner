using System.Linq;
using Controllers;
using MB;
using Profile;
using Tool.Levels;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private DiamondScanner _diamondScanner;
    [SerializeField] private EnemyScanner _enemyScanner;
    [SerializeField] private TileMapScanner _tileMapScanner;

    [Header("Level Manager")]
    [SerializeField] private LevelManager _levelManager;

    private Player _player;

    private ProfilePlayers _profilePlayer;
    
    private MainController _mainController;

    private GameLevel _gameLevel;

    private void Awake()
    {
        GetPlayer();
        _profilePlayer = new ProfilePlayers(GameState.MainMenu);
        _gameLevel = new LevelHandler().GetGameLevel();
        _mainController = new MainController(_profilePlayer, _placeForUi, _gameLevel, _player, _diamondScanner, _enemyScanner, _tileMapScanner, _levelManager);
    }

    private void GetPlayer()
    {
        _player = (FindObjectsOfType(typeof(Player)) as Player[]).FirstOrDefault();
        _player.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }

    public void Update()
    {
        var deltaTime = Time.deltaTime;
        _mainController.Update(deltaTime);
    }
}
