using Profile;
using Tool;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using View;

namespace Controllers.UI
{
    internal class ExitController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/ExitMenu");

        private ExitMenuView _exitMenuView;
        private ProfilePlayers _profilePlayer;
        private AudioEffectsManager _audioEffectsManager;
        private AudioSource _audioSource;

        private Button _exitButton;
        private Button _backButton;

        public ExitController(Transform placeForUi, ProfilePlayers profilePlayer, AudioEffectsManager audioEffectsManager, AudioSource audioSource)
        {
            _exitMenuView = LoadView(placeForUi);
            _profilePlayer = profilePlayer;
            _audioEffectsManager = audioEffectsManager;
            _audioSource = audioSource;
            AddButton();
            SubscribeButton();
        }

        private ExitMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ExitMenuView>();
        }
        private void AddButton()
        {
            _exitButton = _exitMenuView.ExitButton;
            _backButton = _exitMenuView.BackButton;
        }

        private void SubscribeButton()
        {
            _exitButton.onClick.AddListener(OnExitButtonClick);
            _backButton.onClick.AddListener(OnBackButtonClick);
        }

        private void OnBackButtonClick()
        {
            AudioButtonClick();
            _profilePlayer.CurrentState.Value = GameState.MainMenu;
        }

        private void UnsubscribeButton()
        {
            _exitButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }

        private void OnExitButtonClick()
        {
            SaveManagement.SetRestart(0);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
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
