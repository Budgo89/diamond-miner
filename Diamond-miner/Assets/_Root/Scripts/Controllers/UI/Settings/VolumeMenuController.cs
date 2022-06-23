using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Controllers
{
    internal class VolumeMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/VolumeMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;

        private VolumeMenuView _volumeMenuView;

        private Button _backButton;

        public VolumeMenuController(Transform placeForUi, ProfilePlayers profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _volumeMenuView = LoadView(placeForUi);
            AddButtons();
            SubscribeButton();
        }

        private void AddButtons()
        {
            _backButton = _volumeMenuView.BackButton;
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

        private VolumeMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<VolumeMenuView>();
        }
    }
}