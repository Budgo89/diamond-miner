using Controllers;
using MB;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Audio;

namespace Scripts
{
    internal class EntryPointGame : MonoBehaviour
    {
        [Header("Scene Objects")]
        [SerializeField] private Transform _placeForUi;
        [SerializeField] private TileMapScanner _tileMapScanner;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioEffectsManager _audioEffectsManager;
        [SerializeField] private SwipeDetection _swipeDetection;

        [Header("Level Manager")]
        [SerializeField] private LevelManager _levelManager;

        private GameLevel _gameLevel;

        private PauseManager _pauseManager;

        private GameController _gameController;
        private AudioSource _audioSourceEffects;

        public void Start()
        {
            _gameLevel = SaveManagement.GetLevels();
            _pauseManager = new PauseManager();
            _audioSourceEffects = GameObject.FindGameObjectWithTag("AudioEffects").GetComponent<AudioSource>();
            _gameController = new GameController(_placeForUi, _tileMapScanner, _levelManager, _gameLevel, _pauseManager, _audioEffectsManager, _swipeDetection, _audioSourceEffects);
        }

        public void Update()
        {
            if (_pauseManager.IsPause())
            {
                return;
            }
            var deltaTime = Time.deltaTime;
            _gameController.Update(deltaTime);
        }
    }
}
