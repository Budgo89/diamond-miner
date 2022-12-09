using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerPhoton : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform[] _position;

    void Start()
    {
        Vector3 pos = _position[Random.Range(0, _position.Length)].position;
        PhotonNetwork.Instantiate(_playerPrefab.name, pos, Quaternion.identity);
    }
    
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    
}
