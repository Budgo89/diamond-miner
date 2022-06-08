using System.Linq;
using Controllers;
using MB;
using UnityEngine;
using UnityEngine.Tilemaps;

internal class EntryPoint : MonoBehaviour
{

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private Tilemap _tileMap;
    private Player _player;

    private MainController _mainController;

    private void Awake()
    {
        GetPlayer();
        _mainController = new MainController(_placeForUi, _player, _tileMap);
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
