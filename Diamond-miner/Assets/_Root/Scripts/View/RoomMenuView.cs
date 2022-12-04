using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomMenuView : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _connectButton;
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private TMP_Text _player1Text;
    [SerializeField] private TMP_Text _player2Text;
    [SerializeField] private GameObject _roomText;
    [SerializeField] private TMP_InputField _nameRoomInputField;

    public Button BackButton => _backButton;
    public Button StartButton => _startButton;
    public Button CreateButton => _createButton;
    public Button ConnectButton => _connectButton;
    public GameObject LockImage => _lockImage;
    public TMP_Text Player1Text => _player1Text;
    public TMP_Text Player2Text => _player2Text;
    public GameObject RoomText => _roomText;

    public bool _isReadiness1 = false;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        SwitchingButtons();
        _player1Text.text = PhotonNetwork.PlayerList[0].NickName;
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            _player2Text.text = PhotonNetwork.PlayerList[1].NickName;
            _lockImage.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(true);
            StartCoroutine(TestCoroutine());
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _player1Text.text = PhotonNetwork.PlayerList[0].NickName;
        _player2Text.text = newPlayer.NickName;
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            _lockImage.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(true);
            StartCoroutine(TestCoroutine());
        }
        //_lockImage.gameObject.SetActive(false);
        //_startButton.gameObject.SetActive(true);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _player2Text.text = "ќжидаем";
        _lockImage.gameObject.SetActive(true);
        _startButton.gameObject.SetActive(false);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();

    }
    
    public override void OnConnectedToMaster()
    {

    }

    private void SwitchingButtons()
    {
        _createButton.gameObject.SetActive(false);
        _connectButton.gameObject.SetActive(false);
        _roomText.gameObject.SetActive(true);
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void StartOnline()
    {
        _isReadiness1 = true;
        StartCoroutine(TestCoroutine());
    }
    private bool isTest = true;
    private void Update()
    {
        if (isTest)
            return;

        if(_isReadiness1)
        {
            PhotonNetwork.LoadLevel("GameSceneOnlain");
            isTest = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_isReadiness1);

        }
        else
        {
            _isReadiness1 = (bool)stream.ReceiveNext();
        }
    }

    IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.LoadLevel("GameSceneOnlain");
        isTest = false;
        StopCoroutine(TestCoroutine());
    }
}
