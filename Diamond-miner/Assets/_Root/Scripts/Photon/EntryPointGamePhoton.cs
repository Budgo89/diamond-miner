using Controllers;
using MB;
using Photon.Pun;
using Profile;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;
using View;

public class EntryPointGamePhoton : MonoBehaviour
{
    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private TileMapScanner _tileMapScanner;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioEffectsManager _audioEffectsManager;
    [SerializeField] private SwipeDetection _swipeDetection;

    [SerializeField] private LevelView _levelView;
    [SerializeField] private Player _player;

    private GameLevel _gameLevel;

    private PauseManager _pauseManager;

    private GameController _gameController;
    private AudioSource _audioSourceEffects;

    [SerializeField] private PhotonView _photonViewView;

    public void Start()
    {
        //if (!_photonViewView.IsMine)
        //{
        //    return;
        //}
        _gameLevel = null;
        _pauseManager = new PauseManager();
        _audioSourceEffects = GameObject.FindGameObjectWithTag("AudioEffects").GetComponent<AudioSource>();
        _gameController = new GameController(_placeForUi, _tileMapScanner, _gameLevel, _pauseManager, _audioEffectsManager, _swipeDetection, _audioSourceEffects, _levelView, _player, _photonViewView);
    }

    public void Update()
    {
        //if (!_photonViewView.IsMine)
        //{
        //    return;
        //}
        if (_pauseManager.IsPause())
        {
            return;
        }
        var deltaTime = Time.deltaTime;
        _gameController.Update(deltaTime);
    }

}
