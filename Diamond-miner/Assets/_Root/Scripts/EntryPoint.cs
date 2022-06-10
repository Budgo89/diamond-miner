using System.Collections.Generic;
using System.Linq;
using Controllers;
using MB;
using Profile;
using UnityEngine;
using UnityEngine.Tilemaps;

internal class EntryPoint : MonoBehaviour
{

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private DiamondScanner _diamondScanner;
    [SerializeField] private EnemyScanner _enemyScanner;
    private Player _player;

    private ProfilePlayers _profilePlayer;
    
    private MainController _mainController;

    private void Awake()
    {
        GetPlayer();
        _profilePlayer = new ProfilePlayers(GameState.MainMenu);
        _mainController = new MainController(_profilePlayer, _placeForUi, _player, _tileMap, _diamondScanner, _enemyScanner);
    }

    private void GetPlayer()
    {
        _player = (FindObjectsOfType(typeof(Player)) as Player[]).FirstOrDefault();
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }

    public void Update()
    {
        _mainController.Update();
    }
}
