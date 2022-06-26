using System;
using MB;
using Profile;
using TMPro;
using Tool;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using View;
using Object = UnityEngine.Object;

namespace Controllers.UI
{
    public class GameUIController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/GameUI");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;
        private DiamondScanner _diamondScanner;
        private GameUIView _gameUIView;
        private DiamondController _diamondController;
        private PauseManager _pauseManager;

        private Button _pauseMenuButton;
        private TMP_Text _diamondСounts;
        private TMP_Text _gameTime;

        private TimeSpan _time;
        private float _seconds;

        public GameUIController(Transform placeForUi, ProfilePlayers profilePlayer, DiamondScanner diamondScanner, DiamondController diamondController, PauseManager pauseManager)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _diamondScanner = diamondScanner;
            _diamondController = diamondController;
            _gameUIView = LoadView(placeForUi);
            _pauseManager = pauseManager;

            AddElementsUi();
            SubscribeButton();

            DiamondСounts();

            _diamondController.DiamondRaised += DiamondСounts;
           
        }

        public void Update(float deltaTime)
        {
            DisplayTimer(deltaTime);
        }

        private void DiamondСounts()
        {
            _diamondСounts.text = _diamondScanner.GetDiamonds().Count.ToString();
        }

        private void SubscribeButton()
        {
            _pauseMenuButton.onClick.AddListener(OnPauseMenuButtonClick);
        }

        private void DisplayTimer(float deltaTime)
        {
            _seconds += deltaTime;
            _time = TimeSpan.FromSeconds(_seconds);

            _gameTime.text = $"{_time.Hours:00}:{_time.Minutes:00}:{_time.Seconds:00}";
        }

        private void OnPauseMenuButtonClick()
        {
            _pauseManager.EnablePause();
        }

        private void UnsubscribeButton()
        {
            _pauseMenuButton.onClick.RemoveAllListeners();
        }
        private void AddElementsUi()
        {
            _pauseMenuButton = _gameUIView.PauseMenuButton;
            _gameTime = _gameUIView.GameTime;
            _diamondСounts = _gameUIView.DiamondСounts;
        }

        private GameUIView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameUIView>();
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }
    }
}
