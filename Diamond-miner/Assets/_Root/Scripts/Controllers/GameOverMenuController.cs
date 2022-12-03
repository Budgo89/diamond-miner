using Profile;
using Tool;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using View;

namespace Controllers
{
    internal class GameOverMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/GameOverMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;
        private AudioEffectsManager _audioEffectsManager;
        private AudioSource _audioSource;

        private GameOverMenuView _gameOverMenuView;

        private Button _restartButton;
        private Button _mainMenuButton;

        public GameOverMenuController(Transform placeForUi, ProfilePlayers profilePlayer, AudioEffectsManager audioEffectsManager, AudioSource audioSource)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _audioEffectsManager = audioEffectsManager;
            _audioSource = audioSource;

            _gameOverMenuView = LoadView(placeForUi);
            AddButton();
            SubscribeButton();
        }

        private void AddButton()
        {
            _restartButton = _gameOverMenuView.RestartButton;
            _mainMenuButton = _gameOverMenuView.MainMenuButton;
        }

        private void SubscribeButton()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        private void OnMainMenuButtonClick()
        {
            AudioButtonClick();
            _profilePlayer.CurrentState.Value = GameState.MainMenu;
        }

        private void OnRestartButtonClick()
        {
            AudioButtonClick();
            SceneManager.LoadScene(1);
        }

        private void UnsubscribeButton()
        {
            _restartButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }

        private GameOverMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameOverMenuView>();
        }
        private void AudioButtonClick()
        {
            _audioSource.clip = _audioEffectsManager.ButtonClick;
            _audioSource.Play();
        }
    }
}