using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using Profile;
using TMPro;
using Tool;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using View;
using EntityKey = PlayFab.MultiplayerModels.EntityKey;

namespace Controllers
{
    internal class RoomController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/RoomMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;
        private AudioEffectsManager _audioEffectsManager;
        private AudioSource _audioSource;

        private RoomMenuView _roomMenuView;

        private Button _backButton;
        private Button _startButton;
        private Button _createButton;
        private Button _connectButton;
        private GameObject _lockImage;
        private TMP_Text _player1Text;
        private TMP_Text _player2Text;
        private GameObject _roomText;
        private TMP_InputField _nameRoomInputField;

        public RoomController(Transform placeForUi, ProfilePlayers profilePlayer, AudioEffectsManager audioEffectsManager, AudioSource audioSource)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _audioEffectsManager = audioEffectsManager;
            _audioSource = audioSource;
            _roomMenuView = LoadView(_placeForUi);

            AddElements();
            SubscribeButton();

            //GetPlayerName();
        }

        private void GetPlayerName()
        {
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                

                PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), request =>
                    {
                        _player1Text.text = request.AccountInfo.PlayFabId;

                    }, error =>
                    {
                        Debug.Log("Got error retrieving user data:");
                        Debug.Log(error.GenerateErrorReport());
                    }
                );
            }
            
        }

        private void AddElements()
        {
            _startButton = _roomMenuView.StartButton;
            _backButton = _roomMenuView.BackButton;
            _createButton = _roomMenuView.CreateButton;
            _connectButton = _roomMenuView.ConnectButton;
            _lockImage = _roomMenuView.LockImage;
            _player1Text = _roomMenuView.Player1Text;
            _player2Text = _roomMenuView.Player2Text;
            _roomText = _roomMenuView.RoomText;
        }

        private void SubscribeButton()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _backButton.onClick.AddListener(OnBackButtonClick);
            _createButton.onClick.AddListener(OnCreateButtonClick);
            _connectButton.onClick.AddListener(OnConnectButtonClick);
        }

        private void OnConnectButtonClick()
        {
            AudioButtonClick();
            _roomMenuView.JoinRoom();
            //SwitchingButtons();
        }

        private void OnCreateButtonClick()
        {
            AudioButtonClick();
            //SwitchingButtons();
            
            _roomMenuView.CreateRoom();
            //_roomMenuView.PlayerCount();

        }

        private void OnStartButtonClick()
        {
            AudioButtonClick();
            _roomMenuView.StartOnline();
        }

        private void OnBackButtonClick()
        {
            AudioButtonClick();
            _roomMenuView.Leave();
            _profilePlayer.CurrentState.Value = GameState.MainMenu;
        }

        private void UnsubscribeButton()
        {
            _startButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
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


        private RoomMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<RoomMenuView>();
        }

        private void SwitchingButtons()
        {
             _createButton.gameObject.SetActive(false);
            _connectButton.gameObject.SetActive(false);
            _roomText.gameObject.SetActive(true);
        }
    }
}