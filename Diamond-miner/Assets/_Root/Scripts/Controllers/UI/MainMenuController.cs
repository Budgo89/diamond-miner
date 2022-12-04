using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Controllers.UI
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/MainMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;
        private AudioEffectsManager _audioEffectsManager;
        private AudioSource _audioSource;

        private MainMenuView _mainMenuView;

        private Button _startGameButton;
        private Button _levelButton;
        private Button _settingsButton;
        private Button _exitButton;
        private Button _onlineButton;

        public MainMenuController(Transform placeForUi, ProfilePlayers profilePlayer, AudioEffectsManager audioEffectsManager, AudioSource audioSource)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _audioEffectsManager = audioEffectsManager;
            _audioSource = audioSource;

            _mainMenuView = LoadView(placeForUi);
            AddButton();
            SubscribeButton();
            
        }
        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void AddButton()
        {
            _startGameButton = _mainMenuView.StartGameButton;
            _levelButton = _mainMenuView.LevelButton;
            _settingsButton = _mainMenuView.SettingsButton;
            _exitButton = _mainMenuView.ExitButton;
            _onlineButton = _mainMenuView.OnlineButton;
        }

        private void SubscribeButton()
        {
            _startGameButton.onClick.AddListener(OnStartGameButtonClick);
            _levelButton.onClick.AddListener(OnLevelButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
            _onlineButton.onClick.AddListener(OnOnlineButtonClick);
        }

        private void OnOnlineButtonClick()
        {
            AudioButtonClick();
            _profilePlayer.CurrentState.Value = GameState.Room;
        }

        private void OnStartGameButtonClick()
        {
            AudioButtonClick();
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

        private void OnLevelButtonClick()
        {
            AudioButtonClick();
            _profilePlayer.CurrentState.Value = GameState.LevelMenu;
        }

        private void OnSettingsButtonClick()
        {
            AudioButtonClick();
            _profilePlayer.CurrentState.Value = GameState.SettingsMenu;
        }

        private void OnExitButtonClick()
        {
            AudioButtonClick();
            _profilePlayer.CurrentState.Value = GameState.ExitMenu;
        }

        private void UnsubscribeButton()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _levelButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _onlineButton.onClick.RemoveAllListeners();
        }

        private void AudioButtonClick()
        {
            _audioSource.clip = _audioEffectsManager.ButtonClick;
            _audioSource.Play();
        }
        protected override void OnDispose()
        {
            UnsubscribeButton();
        }
    }
}