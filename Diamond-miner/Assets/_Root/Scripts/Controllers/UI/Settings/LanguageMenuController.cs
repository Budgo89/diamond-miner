using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Controllers
{
    internal class LanguageMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/LanguageMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;

        private LanguageMenuView _languageMenuView;

        private Button _backButton;

        public LanguageMenuController(Transform placeForUi, ProfilePlayers profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _languageMenuView = LoadView(placeForUi);
            AddButtons();
            SubscribeButton();
        }

        private void AddButtons()
        {
            _backButton = _languageMenuView.BackButton;
        }

        private void SubscribeButton()
        {
            _backButton.onClick.AddListener(OnBackButtonClick);
        }

        private void OnBackButtonClick() => _profilePlayer.CurrentState.Value = GameState.SettingsMenu;

        private void UnsubscribeButton()
        {
            _backButton.onClick.RemoveAllListeners();
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }

        private LanguageMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<LanguageMenuView>();
        }
    }
}